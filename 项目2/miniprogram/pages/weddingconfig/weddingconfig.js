// miniprogram/pages/weddingconfig/weddingconfig.js
const app = getApp();
var islock=true;
var horizontalNum=0;//横屏图片
var verticalNum=0;//竖屏图片
var templetId="";//样板ID
Page({

  /**
   * 页面的初始数据
   */
  data: {
    tempImagesSix:[],//选择图片预览图url
    tempImagesFour: [],//选择图片预览图url
    hidebase:'',//隐藏 基本资料tab
    basebottom:'border-bottom: 2px solid #abcdef;',//tab样式
    hideimage:'hidden',//隐藏 图片资料tab
    imagebottom: '',//tab样式
    _id:'',//数据ID 
    horizontalNum:0,//横屏图片
    verticalNum : 0,//竖屏图片
  },
  
  //提示窗
  onshowToast:function(text){
    wx.showToast({
      title: text,
      duration: 2000,
      icon: "none"
    })
   islock=true;
  },

  //提交数据
  onSubmit: function (e) {
    var that = this;  
    let str = /^1\d{10}$/; 
    if(islock){
      islock=false;
      if (app.globalData.openid.length < 1) {
        if (e.detail.value.address.length < 10) {
          that.onshowToast('未授权，无法提交');
          return;
        }
      }

      if ((e.detail.value.man_name.length < 2 || e.detail.value.man_name.length > 4)) {
        that.onshowToast('姓名1长度错误');
        return;
      }
      if (!str.test(e.detail.value.man_phone)) {
        that.onshowToast('手机号1格式错误');
        return;
      }
      if ((e.detail.value.women_name.length < 2 || e.detail.value.women_name.length > 4)) {
        that.onshowToast('姓名2长度错误');
        return;
      }

      if (!str.test(e.detail.value.women_phone)) {
        that.onshowToast('手机号2格式错误');
        return;
      }

     
      if (e.detail.value.address.length < 10) {
        that.onshowToast('宴客地址不能为空或者不够详细！');
        return;
      }

      wx.showModal({
        title: '提示',
        content: '提交成功后，修改资料请联系管理员，是否需要提交？',
        showCancel: true,
        success(res) {
          if (res.confirm) {
            const db = wx.cloud.database()
            db.collection('wedding_config').add({
              data: {
                address: e.detail.value.address,
                man_name: e.detail.value.man_name,
                man_phone: e.detail.value.man_phone, 
                wedding_openid: app.globalData.openid,
                women_name: e.detail.value.women_name,
                women_phone: e.detail.value.women_phone,
                templetId: templetId,
                time: new Date()
              },
              success: function (res) {
                that.onshowToast('提交成功');
                that.setData({
                  hideimage: '',
                  hidebase: 'hidden',
                  basebottom: '',
                  imagebottom: 'border-bottom: 2px solid #abcdef;',
                  _id: res._id,
                }) 
                console.log("_id:" + res._id) 
              },
              fail: function () {
                that.onshowToast('系统繁忙请稍后提交或联系管理员');
              }
            })
          } else if (res.cancel) {

          }
        }
      })

    }

  },
 
  //选择图片
  onChooseImage: function (e) {
    var that = this; 
    var num = e.target.dataset.num 
    wx.chooseImage({//选择图片
      count: num,
      sizeType: ['original', 'compressed'],
      sourceType: ['album', 'camera'],
      success: function (res) {
        if (num == horizontalNum) {//如果是选择N张的按钮，先判断是否是N张图片，是就清空显示新选择的图片，否就提示
          if (res.tempFilePaths.length != horizontalNum) {
            that.onshowToast('请选择' + horizontalNum+'张横屏图片'); 
            return;
          }else
          {
            that.setData({
              tempImagesSix: [],
            });  
          } 
        }
        if (num ==verticalNum) {
          if (res.tempFilePaths.length != verticalNum) {
            that.onshowToast('请选择' + verticalNum+'张竖屏图片'); 
            return;
          }else{
            that.setData({
              tempImagesFour: [],
            });  
          } 
        }
       
        for(var i=0;i<res.tempFilePaths.length;i++){
          var filePath = res.tempFilePaths[i];
          var newarray = { url: filePath};
          if (num == horizontalNum){ 
            that.setData({
              tempImagesSix: that.data.tempImagesSix.concat(newarray),
            }); 
          }
          if (num == verticalNum){ 
            that.setData({
              tempImagesFour: that.data.tempImagesFour.concat(newarray),
            });
          }
 
        } 
      }
    })
  },

  
  //上传图片
  onUpdateimg:function(){
    var that=this;
    if (app.globalData.openid.length < 1) {
      if (e.detail.value.address.length < 10) {
        that.onshowToast('未授权，无法提交');
        return;
      }
    }
    if (that.data._id.length<1){
      if (e.detail.value.address.length < 10) {
        that.onshowToast('请先提交基本资料');
        return;
      }
    }
    if (that.data.tempImagesSix.length <= 0 || that.data.tempImagesFour<=0){ 
        that.onshowToast('请选择图片');
        return; 
    } 
    wx.showModal({
      title: '提示',
      content: '提交成功后，修改资料请联系管理员，是否需要提交？',
      showCancel: true,
      success(res) {
        if (res.confirm) {
          var updaTeileidSix=[];
          for (var i = 0; i < that.data.tempImagesSix.length;i++){
            var cloudPath = app.globalData.openid + "/" + Date.parse(new Date()) + i +  that.data.tempImagesSix[i].url.match(/\.[^.]+?$/)[0];
            console.log("cloudPath:" + cloudPath)
            console.log("filePath:" + that.data.tempImagesSix[i].url)
            wx.cloud.uploadFile({//上传图片
              cloudPath,
              filePath: that.data.tempImagesSix[i].url,  
              success: function (res) { 
                var newarray = { fileID: res.fileID }; 
                updaTeileidSix.push(newarray);
                console.log("上传成功：updaTeileidSix" + updaTeileidSix); 
              },
              fail: function (res) {
                console.log("上传失败：" + res.errMsg + ";code:" + res.errCode)
              }
            })
          }
           var updaTeileidFour = [];
          for (var i = 0; i < that.data.tempImagesFour.length; i++) {
            var cloudPath = app.globalData.openid + "/" + Date.parse(new Date()) +i+ that.data.tempImagesFour[i].url.match(/\.[^.]+?$/)[0];
            wx.cloud.uploadFile({//上传图片
              cloudPath,
              filePath: that.data.tempImagesFour[i].url,
              success: function (res) {
                var newarray = { fileID: res.fileID };
                updaTeileidFour.push(newarray);

                console.log("上传成功：updaTeileidFour" + updaTeileidFour);
              },
              fail: function (res) {
                console.log("上传失败：" + res.errMsg + ";code:" + res.errCode)
              }
            })
          } 
          wx.showLoading({
            title: '上传进度：0%'
          }) 
          var interval = setInterval(function () {//定时检测上传进度
            if (updaTeileidSix.length + updaTeileidFour.length == horizontalNum+verticalNum) {
              clearInterval(interval);
              console.log("updaTeileidSix:" + updaTeileidSix)
              console.log("updaTeileidFour:" + updaTeileidFour)
              const db = wx.cloud.database()
              db.collection('wedding_config').doc(that.data._id).update({
                data: {
                  page3_six: updaTeileidSix,
                  page4_four: updaTeileidFour,
                },
                success: function (res) {
                  that.onshowToast('提交成功,等待管理员审核!');
                  setTimeout(function () {
                    wx.switchTab({
                      url: "/pages/user/user"
                    })
                  }, 2000)
                },
                fail: function () {
                  that.onshowToast('系统繁忙请稍后提交或联系管理员');
                }
              })
              wx.hideLoading()
            } 
            wx.showLoading({
              title: '上传进度：' + ((10.00 / (horizontalNum + verticalNum)) * (updaTeileidSix.length + updaTeileidFour.length) * 10).toFixed(0)+ '%'
            }) 
          }, 1000) 
           
        
        } else if (res.cancel) {

        }
      }
    }) 
  },

  // 获取配置信息
  onGetWeddingConfig() {
    var that = this;
    const db = wx.cloud.database()
    db.collection('wedding_config').where({
      wedding_openid: app.globalData.openid,
    }).get({ 
      success: res => {
        that.setData({
          hideimage: '',
          hidebase: 'hidden',
          basebottom: '',
          imagebottom: 'border-bottom: 2px solid #abcdef;',
          _id: res.data[0]._id,
        }) 
      },
      fail: err => {
        console.error('[数据库] [查询记录] 失败：', err)
      }
    })

  },
 

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) { 
    templetId = options.templetId;//样板ID
    horizontalNum = options.horizontalNum;//横屏图片
    verticalNum = options.verticalNum;//竖屏图片
    this.setData({
      horizontalNum :options.horizontalNum,//横屏图片
      verticalNum: options.verticalNum//竖屏图片
    })
    this.onGetWeddingConfig();// 获取配置信息
  },

  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function () {

  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function () {

  },

  /**
   * 生命周期函数--监听页面隐藏
   */
  onHide: function () {

  },

  /**
   * 生命周期函数--监听页面卸载
   */
  onUnload: function () {

  },

  /**
   * 页面相关事件处理函数--监听用户下拉动作
   */
  onPullDownRefresh: function () {

  },

  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function () {

  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {

  }
})