// miniprogram/pages/csstest5/csstest5.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    indicatorDots: true,//是否显示面板指示点
    autoplay: false,//是否自动切换
    circular: true,//是否采用衔接滑动
    vertical: true,//滑动方向是否为纵向
    interval: 600,//自动切换时间间隔
    duration: 600,//滑动动画时长
    previousMargin: '0',//前边距，可用于露出前一项的一小部分
    nextMargin: '0',//后边距，可用于露出后一项的一小部分
    
    windowWidth: 0,//当前屏幕宽度
    windowHeight: 0,//当前屏幕高度  
    seat: [],//星星位置
    poptop:'100%',//弹窗top值
    poptext:'open',//弹窗文本
  },

  //产生星星位置
  onstar: function () {
    var that = this;
    var starclass = ['star', 'star1', 'star2', 'star3'];
    for (var i = 0; i < 60; i++) {
      var width = Math.floor(Math.random() * (that.data.windowWidth * 2.3));
      var height = Math.floor(Math.random() * (that.data.windowHeight * 2.3));
      var classid = Math.floor(Math.random() * 4);
      var animationdelay = Math.floor(Math.random() * 20);
      var animationtime = Math.floor(Math.random() * 6);
      var newarray = { starclass: starclass[classid], top: height, left: width, animation: "animation: animation_star " + animationtime + "s ease-in-out " + animationdelay + "s infinite;" };
      that.setData({
        seat: that.data.seat.concat(newarray),
      });
    }
    console.log("seat：" + JSON.stringify(that.data.seat));
  },

  //弹窗事件
  onopen:function(){
    var that = this;
    if(that.data.poptop=='100%'){
      that.setData({
        poptop: '45%',
        poptext:'close'
      });
    }else{
      that.setData({
        poptop: '100%',
        poptext: 'open'
      });
    }

  },

  //禁止页面滑动
  onCatchtouchmove:function(){},


  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    var that = this;
    wx.getSystemInfo({//获取系统信息方法
      success: function (res) {
        that.setData({
          windowWidth: res.windowWidth,
          windowHeight: res.windowHeight,
        })
        console.log("cells:" + JSON.stringify(res));
      }
    })
    that.onstar();
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