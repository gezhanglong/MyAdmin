// miniprogram/pages/myfirst/myfirst.js
const app = getApp();

Page({

  /**
   * 页面的初始数据
   */
  data: {
    windowWidth: 0,//当前屏幕宽度
    windowHeight: 0,//当前屏幕高度 
    seat: [],//星星位置
    headurl:"",
    nickname:"",
    openid:"",
    unionid:"",  
    allconfig:[], 
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
    that.onstar();
    this.onAllConfig();//获取首页显示的配置信息 
  },

  //产生星星位置
  onstar: function () {
    var that = this;
    var starclass = ['star', 'star1', 'star2', 'star3'];
    for (var i = 0; i < 100; i++) {
      var width = Math.floor(Math.random() * (that.data.windowWidth * 2));
      var height = Math.floor(Math.random() * (that.data.windowHeight * 2));
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
  onlog:function(logtype){
    var that=this;
    const db = wx.cloud.database()
    db.collection('all_log').add({ //添加数据
      data: {
        headurl: that.data.headurl, 
        nickname: that.data.nickname,
        openid:that.data.openid,
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
  onAllConfig:function(){
    var that = this;
    const db = wx.cloud.database()
    db.collection('all_config').where({
      isShow: true
    }).get({
      success(res) { 
        if (res.data.length >= 1) { 
          that.setData({
            allconfig:res.data,
          }) 
        }
        console.log("that.data.allconfig:"+that.data.allconfig)
      },
      fail: err => {
        console.error('[数据库] [查询记录] 失败：', err)
      }
    }) 
  },

  //邀请函邀请者
  // onNavigateToWedding: function () {
  //   var that = this;
  //   if (that.data.openid <= 0) {
  //     wx.showToast({
  //       icon: 'none',
  //       title: '请授权后访问'
  //     })
  //     return;
  //   }  
  //   wx.navigateTo({
  //     url: '/pages/wedding/wedding?wedding_openid=' + that.data.openid
  //   })
  // },
  
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
      url: "/pages/wedding/wedding?wedding_openid=" + e.target.dataset.templetid + "&templetId=" + e.target.dataset.templetid +"&istemplet=0"
    })
  },

  //邀请函邀请人配置页
  // onNavigateToWeddingConfig: function () {
  //   var that = this;
  //   if (that.data.openid <= 0) {
  //     wx.showToast({
  //       icon: 'none',
  //       title: '请授权后访问'
  //     })
  //     return;
  //   }
  //   wx.navigateTo({
  //     url: '/pages/weddingabout/weddingabout'
  //   })
  // },

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
  

})