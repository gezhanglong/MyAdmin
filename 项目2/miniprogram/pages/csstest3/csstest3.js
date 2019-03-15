// miniprogram/pages/csstest3/csstest3.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    windowWidth: 0,//当前屏幕宽度
    windowHeight: 0,//当前屏幕高度 
    startY: 0,  //当前开始点击位置
    scrollTop:0,//当前屏幕位置
    animation:'',//动画

    markers: [{
      iconPath: '../../images/icon_1.png',
      id: 0,
      latitude: 23.099994,
      longitude: 113.324520,
      width: 50,
      height: 50
    }],
    polyline: [{
      points: [{
        longitude: 113.3245211,
        latitude: 23.10229
      }, {
        longitude: 113.324520,
        latitude: 23.21229
      }],
      color: '#FF0000DD',
      width: 2,
      dottedLine: true
    }],
    controls: [{
      id: 1,
      iconPath: '../../images/icon_1.png',
      position: {
        left: 0,
        top: 300 - 50,
        width: 50,
        height: 50
      },
      clickable: true
    }],

    poster: 'http://y.gtimg.cn/music/photo_new/T002R300x300M000003rsKF44GyaSk.jpg?max_age=2592000',
    name: '此时此刻',
    author: '许巍',
    src: 'https://7a6c-zltest-d3262f-1258495345.tcb.qcloud.la/陈坤-寻龙诀-(电影《寻龙诀》主题曲).mp3?sign=e79d315a1788f86d1f5ecf83fbe4f753&t=1552639934',
  },

  audioPlay() {
    this.audioCtx.play()
  },
  audioPause() {
    this.audioCtx.pause()
  },
  audio14() {
    this.audioCtx.seek(14)
  },
  audioStart() {
    this.audioCtx.seek(0)
  },

 
  //点击开始事件
  onStart:function(event){
    var that = this;
    console.log("动了前：" + JSON.stringify(event));
    that.setData({ 
      startY: event.touches[0].pageY, //获得点击高度位置
    })
  },
  
  //点击结束事件
  onEnd: function (event) {
    var that = this;
    console.log("动了后：" + JSON.stringify(event)); 
    var endY = event.changedTouches[0].pageY 
    var ty = that.data.startY-endY // 点击开始位置-点击结束位置 如果是正数页面往上滑；如果为负数页面往下滑
    var scrollTop = that.data.scrollTop;
    console.log("ty:" + ty + ";scrollTop:" + scrollTop);
    if (ty < 0 && scrollTop>0)//页面往下滑并且当前屏幕位置不在顶部
    {
      scrollTop = scrollTop - that.data.windowHeight;
    } else if (ty > 0 && scrollTop < that.data.windowHeight * 2)//页面往上滑并且当前屏幕位置不在底部
    {
      scrollTop = scrollTop + that.data.windowHeight;
    }else
    {
      return;
    } 

    that.setData({
      scrollTop: scrollTop,//更新当前屏幕位置
    })
   
    //将页面滚动到目标位置
    // wx.pageScrollTo({
    //   scrollTop: scrollTop,
    //   duration: 1500,
    //   success: function (e) {
    //     if (scrollTop == that.data.windowHeight)
    //     {
    //       that.setData({
    //         animation: "animation: myfirst 2s linear",
    //       })
    //     }else{
    //       that.setData({
    //         animation: "",
    //       })
    //     }
    //     console.log("调用成功：" + JSON.stringify(e));
    //   },
    //   fail:function(e){
    //     console.log("调用失败：" + JSON.stringify(e));
    //   },
    // }) 
    console.log("动了后："+scrollTop);
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
          windowHeight: res.screenHeight ,
        })
        console.log("cells:" + JSON.stringify(res));
      }
    })
  },

  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function () {
    this.audioCtx = wx.createAudioContext('myAudio')
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