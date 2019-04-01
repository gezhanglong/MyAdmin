// miniprogram/pages/csstest4/csstest4.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    windowWidth: 0,//当前屏幕宽度
    windowHeight: 0,//当前屏幕高度 
    indicatorDots: false,//是否显示面板指示点
    autoplay: false,//是否自动切换
    circular: false,//是否采用衔接滑动
    vertical: false,//是否采用衔接滑动
    interval: 6000,//自动切换时间间隔
    duration: 600,//滑动动画时长
    previousMargin: '0',//前边距，可用于露出前一项的一小部分
    nextMargin: '0',//后边距，可用于露出后一项的一小部分
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    var that = this;
    wx.getSystemInfo({//获取系统信息方法
      success: function (res) {
        that.setData({
          windowWidth: res.screenWidth,
          windowHeight: res.screenHeight,
        })
        console.log("cells:" + JSON.stringify(res));
      }
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