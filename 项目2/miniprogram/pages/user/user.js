// miniprogram/pages/user/user.js
const app = getApp()
Page({

  /**
   * 页面的初始数据
   */
  data: {
    isadmin:false,
    isuser: false, 
    headurl: "",
    nickname: "",
    openid: "",
    unionid: ""
  },

  /**
   * 生命周期函数--监听页面加载  app.globalData.openid
   */
  onLoad: function (options) {
   this.onLogin();
  },

  onLogin:function(){
    this.setData({
      headurl: app.globalData.headurl,
      nickname: app.globalData.nickname.length > 0 ? app.globalData.nickname : "请授权后进入",
      openid: app.globalData.openid,
    });
    var that = this;
    if (that.data.openid <= 0) {
      return;
    }
    const db = wx.cloud.database()
    db.collection('user').where({
      _openid: that.data.openid
    }).get({
      success: res => {

        if (res.data.length <= 0) {
          return
        }
        if (res.data[0].userpower == 0)//0最高权限，1普通权限
        {
          that.setData({
            isuser: true,
            isadmin: true,
          })
        } else {
          that.setData({
            isuser: true,
          })
        }

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

  onUserNavigateTo:function(){ 
    wx.navigateTo({
      url: '/pages/userinfo/userinfo'
    })
  },
  
  onFirstImgNavigateTo: function() {
    wx.navigateTo({
      url: '/pages/updateimgfirst/updateimgfirst'
    })
  }, 
  onImgNavigateTo: function () {
    wx.navigateTo({
      url: '/pages/updateimg/updateimg'
    })
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
    this.onLogin();
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