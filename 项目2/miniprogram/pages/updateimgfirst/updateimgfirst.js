// miniprogram/pages/myinfo/myinfo.js

const app = getApp()
Page({

  /**
   * 页面的初始数据
   */
  data: {
    isuser:false, 
    imgurl:'',
    cloudimgurl:'',
    fileid:'',
    title:'',
    des:'',
    cloudPath1:'',
    filePath1:'',
    imgurls: [],
    ids:[],
    fileList : [],
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {  
    this.onLogin();
    this.onPhoto();
  },

  //到数据库拿管理员信息
  onLogin:function(){
    if (app.globalData.openid <= 0) {
      return;
    }
    const db = wx.cloud.database()
    db.collection('user').where({
      _openid: app.globalData.openid
    }).get({
      success: res => {
        if (res.data.length <= 0) {
          return
        }
        this.setData({
          isuser: true, 
        })
      },
      fail: err => {
        wx.showToast({
          icon: 'none',
          title: '查询记录失败'
        })
        console.error('[数据库] [查询记录] 失败：', err)
      }
    })
  },

  //到数据库拿图片信息
  onPhoto: function () {
    var that = this;
    const db = wx.cloud.database()
    const photoCollection = db.collection('photoinfofirst').where({
      _openid:app.globalData.openid,
    }).get({
      success(res) {  //获取集合并遍历数据
        var fileList = [];
        for (var i = 0; i < res.data.length; i++) {
          fileList.push(res.data[i].imgurl);
          that.setData({
            ids: that.data.ids.concat(res.data[i]._id),
          }); 
        }
        console.log("res:" + JSON.stringify(res.data));
        console.log("fileList:" + fileList);
        if (fileList.length > 0) {
          wx.cloud.getTempFileURL({//下载图片 获得图片地址  
            fileList: fileList,
            success: function (des) {
              console.log("下载成功：" + JSON.stringify(des.fileList));
              for (var j = 0; j < des.fileList.length; j++) { 
                    var newarray = { url: des.fileList[j].tempFileURL };
                    that.setData({
                      imgurls: that.data.imgurls.concat(newarray), 
                      fileList:fileList,
                    }); 
              }

            }
          })
        }

      },
      fail(res) {
        console.log("集合错误：" + res);
      }
    })
  },
  
  //选择图片并上传图片并下载图片
  onUpdateimg:function(){
    var that = this; 
    if (!that.data.isuser) {
      wx.showToast({
        icon: 'none',
        title: '无管理员信息，不能操作上传!'
      })
      return;
    }
    wx.chooseImage({//选择图片
      count:1,
      sizeType: ['original', 'compressed'],
      sourceType: ['album', 'camera'],
      success:function(res){
        console.log(res); 
        const filePath = res.tempFilePaths[0]; 
        const cloudPath = app.globalData.openid + "/" + Date.parse(new Date())+ filePath.match(/\.[^.]+?$/)[0];

        that.setData({
          filePath1: filePath,
          cloudPath1: cloudPath
            })
        wx.cloud.uploadFile({//上传图片
          cloudPath, 
          filePath,
          header: { 
            "Content-Type": "multipart/form-data" 
          },
          success:function(res){ 
            const cloudfileid=res.fileID;
            that.setData({
              imgurl: filePath,
              fileid: cloudfileid,
              previewimgurl:filePath,
            }),
              console.log("上传成功：" + cloudfileid);
            wx.cloud.downloadFile({//下载图片 获得图片地址  
              fileID: that.data.fileid, 
              success:function(res){
                console.log("下载成功：" + res.tempFilePath),
                that.setData({
                  cloudimgurl: res.tempFilePath,
                })
              }
            }) 
          },
          fail: function (res) {
            console.log("上传失败：" + res.errMsg + ";code:" + res.errCode)
          }
        })
       
      }
    })
  },

  //提交数据
  onSubmit:function(e){ 
    var that = this; 
    if(!that.data.isuser)
    {
      wx.showToast({
        icon: 'none',
        title: '无管理员信息，不能操作上传!'
      })
      return;
    } 
    if (that.data.cloudimgurl.length<=0)
    {
      wx.showToast({
        icon: 'none',
        title: '无图片地址信息'
      })
      return;
    }
    const db = wx.cloud.database() 
    db.collection('photoinfofirst').add({
      data:{
        imgurl: that.data.fileid,
        title:e.detail.value.title,
        des:e.detail.value.des,
        time:new Date()
      },
      success:function(res){
        wx.showModal({
          title: '提示',
          content: res.errMsg, 
          showCancel:false,
          success(res) {
            if (res.confirm) {  
              for (var i = 0; i < that.data.ids.length; i++) {
                var id = that.data.ids[i] ;
                db.collection('photoinfofirst').doc(id).remove({//删除数据库数据
                  success: function (res) {
                    console.log("删除成功id：" + id + ";msg:" + JSON.stringify(res));
                  },
                  fail: function (res) {
                    console.log("删除失败id：" +id + ";msg:" + JSON.stringify(res));
                    return;
                  }
                });
              } 
              wx.cloud.deleteFile({//删除存储文件
                fileList:that.data.fileList,
                success: res => { 
                  console.log("删除成功："+res.fileList)
                },
                fail: err => { 
                  console.log("删除失败："+err)
                }
              })
              that.setData({
                imgurl: '',
                cloudimgurl: '',
                fileid: '',
                title: '',
                des: '',
                imgurls:[], 
              })
              wx.showLoading({
                title: '加载中...',
              })  
              setTimeout(function () {
                that.onPhoto();
                wx.hideLoading()
              }, 2000)   
            } else if (res.cancel) {
              
            }
          }
        }) 
      }
    })
  },

  //预览图片
  onPreviewImage: function (e) {
    var current = e.target.dataset.src;
    wx.previewImage({
      current: current, // 当前显示图片的http链接
      urls: [current] // 需要预览的图片http链接列表
    })
  },

  /**
  * 生命周期函数--监听页面显示
  */
  onShow: function () {
    this.onLogin();
  },
})