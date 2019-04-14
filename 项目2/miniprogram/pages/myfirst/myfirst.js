const app = getApp();
Page({

  /**
   * 页面的初始数据
   */
  data: {
    headurl: "",
    nickname: "",
    openid: "",
    unionid: "",
    allconfig: [], 

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

    isnight:true,//true为晚上；false为白天
    moon:0,//从0到105为半个月，一天7格
    suntop: 0,//太阳的top位置 从120到30 1个小时15（除于6）
    sunleft: 0,//太阳的left位置 从-150到700 1个小时间隔71
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

  //计算晚上月亮的角度，白天太阳位置
  oncount:function(){
    var that = this; 
    var today = new Date();
    var day= today.getDate();
    var hour=today.getHours();
    if (hour >= 6 && hour <= 18) {//白天
      var sunleft = -150 + (hour - 6) * 71;
      var suntop = 0;
      if (hour <= 12) {
        suntop = 120 - (hour - 6) * 15
      } else {
        suntop = (hour - 6) * 15
      }

      that.setData({
        isnight: false,
        sunleft: sunleft,
        suntop: suntop,
      }) 
    }else{
      var moon = day * 7;
      if (moon > 105) {
        moon = 105 - (day - 15) * 7;
      }
      that.setData({
        isnight: true,
        moon: moon,
      })
      console.log("moon:" + that.data.moon + ",day:" + day + ",hour:" + hour)
    }
  },

  //授权
  onGotUserInfo(e) {
    this.setData({
      headurl: e.detail.userInfo.avatarUrl,
      nickname: e.detail.userInfo.nickName,
    })
    app.globalData.nickname = e.detail.userInfo.nickName;
    app.globalData.headurl = e.detail.userInfo.avatarUrl
    this.onGetOpenid()//调用云函数
  },



  onGetOpenid: function () {
    // 调用云函数
    wx.cloud.callFunction({
      name: 'login',
      data: {},
      success: res => {
        console.log('[云函数] [login] user openid: ', res.result.openid)
        app.globalData.openid = res.result.openid
        this.setData({
          openid: app.globalData.openid
        })

        this.onlog(0);//0为首页记录

      },
      fail: err => {
        console.error('[云函数] [login] 调用失败', err)
      }
    })
  },


  //添加记录
  onlog: function (logtype) {
    var that = this;
    const db = wx.cloud.database()
    db.collection('all_log').add({ //添加数据
      data: {
        headurl: that.data.headurl,
        nickname: that.data.nickname,
        openid: that.data.openid,
        createtime: new Date(),
        logtype: logtype,
      },
      success: function (res) {
        console.log('[数据库] [添加数据] 成功：', res)
      },
      fail: function (res) {
        console.error('[数据库] [添加数据] 失败：', res)
      }
    })
  },



  ////获取首页显示的配置信息
  onAllConfig: function () {
    var that = this;
    const db = wx.cloud.database()
    db.collection('all_config').where({
      isShow: true
    }).get({
      success(res) {
        if (res.data.length >= 1) {
          that.setData({
            allconfig: res.data,
          })
        }
        console.log("that.data.allconfig:" + that.data.allconfig)
      },
      fail: err => {
        console.error('[数据库] [查询记录] 失败：', err)
      }
    })
  },


  //邀请函模板1
  onNavigateToTemplet1: function (e) {
    var that = this;
    if (that.data.openid <= 0) {
      wx.showToast({
        icon: 'none',
        title: '请授权后访问'
      })
      return;
    }  
    wx.navigateTo({
      url: "/pages/wedding/wedding?wedding_openid=" + e.currentTarget.dataset.templetid + "&templetId=" + e.currentTarget.dataset.templetid + "&istemplet=0"
    })
  },




  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    var that = this;
   
    that.setData({ 
      headurl: app.globalData.headurl,
      nickname: app.globalData.nickname,
      openid: app.globalData.openid,
    }) 
    console.log("openid:" + that.data.openid);
    wx.getSystemInfo({//获取系统信息方法
      success: function (res) {
        that.setData({
          windowWidth: res.windowWidth,
          windowHeight: res.windowHeight, 
        }) 
        console.log("cells:" + JSON.stringify(res));
      }
    })
    this.onstar();  //产生星星位置
    this.oncount();//计算晚上月亮的角度，白天太阳位置
    this.onAllConfig();//获取首页显示的配置信息 
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