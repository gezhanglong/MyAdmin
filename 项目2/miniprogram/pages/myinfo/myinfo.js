// miniprogram/pages/myinfo/myinfo.js

const app = getApp()
Page({

  /**
   * 页面的初始数据
   */
  data: {
    nickname: '', 
    imgurl:'',
    cloudimgurl:'',
    fileid:'',
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) { 
    this.setData({
      nickname:app.globalData.nickname,
    })
    const db = wx.cloud.database()
    db.collection('user').where({
      _openid: app.globalData.openid
    }).get({
      success: res => {
        if (res.data.length<=0){
          wx.showToast({
            icon: 'none',
            title: '无管理员信息'
          })
          return
        }
       
        wx.showToast({
          icon: 'success',
          title: '登录成功'
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
  
  onUpdateimg:function(){
    var that = this;
    wx.chooseImage({//选择图片
      count:1,
      sizeType: ['original', 'compressed'],
      sourceType: ['album', 'camera'],
      success:function(res){
        console.log(res); 
        const filePath = res.tempFilePaths[0];
        wx.cloud.uploadFile({//上传图片
          cloudPath: 'my-image1' + filePath.match(/\.[^.]+?$/)[0],
          filePath: filePath,
          success:function(res){ 
            const cloudfileid=res.fileID;
            that.setData({
              imgurl: filePath,
              fileid: cloudfileid
            }),
              console.log("上传成功：" + cloudfileid);
            wx.cloud.downloadFile({//下载图片 获得图片地址  
              fileID: that.data.fileid, 
              success:function(res){
                console.log("下载成功：" + res.tempFilePath),
                that.setData({
                  cloudimgurl: res.tempFilePath,
                })
              },
              fail:function(res){
                console.log("下载失败："+res.errMsg+";code:"+res.errCode)
              }
            })

          }
        })
       
      }
    })
  },

  onSubmit:function(e){
    var that = this;
    console.log("form:"+e.detail.value.des)
    const db = wx.cloud.database()
    db.collection('user').add({
      data:{
        imgurl: that.data.cloudimgurl,
        title:e.detail.value.title,
        des:e.detail.value.des,
        time:new Date
      },
      success:function(res){
        console.log("提交："+res)
      }
    })
  },
})