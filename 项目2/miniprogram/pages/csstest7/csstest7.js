// miniprogram/pages/csstest4/csstest4.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    windowWidth: 0,//当前屏幕宽度
    windowHeight: 0,//当前屏幕高度 
    indicatorDots: true,//是否显示面板指示点
    autoplay: false,//是否自动切换
    circular: true,//是否采用衔接滑动
    vertical: true,//滑动方向是否为纵向
    interval: 600,//自动切换时间间隔
    duration: 600,//滑动动画时长
    previousMargin: '0',//前边距，可用于露出前一项的一小部分
    nextMargin: '0',//后边距，可用于露出后一项的一小部分 

    page4_img1: 'https://7a6c-zltest-d3262f-1258495345.tcb.qcloud.la/my-image1550656769000.jpg?sign=3533658db71fc51b34deab72f74c5458&t=1555235359',
    page4_img2:'https://7a6c-zltest-d3262f-1258495345.tcb.qcloud.la/my-image1550656713000.jpg?sign=294669fa095c7bca2618e96b744d1c60&t=1555249559',
    page4_img3: 'https://7a6c-zltest-d3262f-1258495345.tcb.qcloud.la/my-image1550656562000.jpg?sign=ba3b596e15cfae809c132c6d25ccffe8&t=1555249583',
    page4_img4: 'https://7a6c-zltest-d3262f-1258495345.tcb.qcloud.la/my-image1550656417000.jpg?sign=da41e6f2c7380ebfc9dfebda10441037&t=1555249604',
    animation_page_4_img: '', //第四屏动画
    page_4_click: 0, //记录点击数值
    page_4_img4: '',
    page_4_img3: '',
    page_4_img2: '',
    page_4_img1: '',
  },

 

 
 

  //初始化第四屏动画
  onSetOnPage_4: function () {
    this.setData({
      page_4_img4: 'animation: animation_page_4 1s forwards  ;animation-fill-mode: both;z-index:7',
      page_4_img3: 'animation: animation_page_4 1s forwards 1.5s;animation-fill-mode: both;z-index:8',
      page_4_img2: 'animation: animation_page_4 1s forwards 3s;animation-fill-mode: both;z-index:9',
      page_4_img1: 'animation: animation_page_4 1s forwards 4.5s;animation-fill-mode: both;z-index:10',
    })
  },

  //清除第四屏动画
  onSetOffPage_4: function () {
    this.setData({
      page_4_img4: '',
      page_4_img3: '',
      page_4_img2: '',
      page_4_img1: '',
    })
  },

  //控制scroll上下移动
  onchange: function (e) {
    console.log("onchange:" + e.detail.current)
    var that = this;
    switch (e.detail.current) {
      case 0: 
        that.onSetOnPage_4(); 
        break; 
    }

  },


  //切换图片
  onpageleft: function (e) {
    var that = this;
    var num = e.target.dataset.click;
    var animation = e.target.dataset.animation
    console.log("num:" + num + "," + num % 3)
    switch (num % 4) {
      case 0:
        console.log("num:" + num + "," + num % 3 + "," + animation)
        that.setData({
          page_4_click: num + 1,
          page_4_img4: 'z-index:10',
          page_4_img3: 'z-index:10',
          page_4_img2: 'z-index:10',
          page_4_img1: 'animation: ' + animation + ' 2s linear;z-index:9',
        })
        break;
      case 1:
        that.setData({
          page_4_click: num + 1,
          page_4_img4: 'z-index:10',
          page_4_img3: 'z-index:10',
          page_4_img2: 'animation: ' + animation + ' 2s linear;z-index:8',
          page_4_img1: 'z-index:9;',
        })
        break;
      case 2:
        that.setData({
          page_4_click: num + 1,
          page_4_img4: 'z-index:10',
          page_4_img3: 'animation: ' + animation + ' 2s linear;z-index:7;',
          page_4_img2: 'z-index:8;',
          page_4_img1: 'z-index:9;',
        })
        break;
      case 3:
        that.setData({
          page_4_click: num + 1,
          page_4_img4: 'animation: ' + animation + ' 2s linear;z-index:6;',
          page_4_img3: 'z-index:7;',
          page_4_img2: 'z-index:8;',
          page_4_img1: 'z-index:9;',
        })
        break;
    }

  },

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
    that.onSetOnPage_4(); 
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