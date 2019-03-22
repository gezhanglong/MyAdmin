// miniprogram/pages/weddingInfo/weddingInfo.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    wedding_openid:'',
    loginDataList: [],
  },

  // 获取用户信息
  onGetLoginData() {
    var that = this;
    const db = wx.cloud.database()
    db.collection('wedding_login').where({
      wedding_openid: that.data.wedding_openid,
    }).orderBy('firsttime','asc').get({ //查询有没有这个openid
      success: res => {
        for (var i = 0; i < res.data.length;i++){
          var info = {
            headurl:res.data[i].headurl, 
            nickname: res.data[i].nickname,
            firsttime: res.data[i].firsttime.getFullYear() + "-" + (res.data[i].firsttime.getMonth() + 1) + "-" + res.data[i].firsttime.getDate() + " " + res.data[i].firsttime.getHours() + ":" + res.data[i].firsttime.getMinutes() + ":" + res.data[i].firsttime.getSeconds(), 
            logintimes: res.data[i].logintimes,
          };
          that.setData({
            loginDataList: that.data.loginDataList.concat(info),
          });
        }
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
    this.setData({
      wedding_openid: options.wedding_openid,
    })
    this.onGetLoginData();
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