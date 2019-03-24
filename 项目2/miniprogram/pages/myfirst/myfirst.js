// miniprogram/pages/myfirst/myfirst.js
const app = getApp();

Page({

  /**
   * 页面的初始数据
   */
  data: {
    headurl:"",
    namenick:"",
    openid:"",
    unionid:"",
    wedding_text:'创建自己的邀请函',
    iswedding:false,
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    var getAppInfo = app.globalData.openid;
    console.log("openid:"+getAppInfo) 
    if (app.globalData.openid=='')
    { 
      wx.getSetting({
        success:res=>{ 
          console.log("authsetting:"+JSON.stringify(res));
          if (res.authSetting['scope.userInfo'])
          {
            wx.getUserInfo({
              success: res => {
                console.log(res.userInfo.nickName); 
                this.setData({
                   headurl: res.userInfo.avatarUrl,
                   namenick: res.userInfo.nickName , 
                   })   
                app.globalData.nickname = res.userInfo.nickName
                app.globalData.headurl = res.userInfo.avatarUrl
                this.onGetOpenid()//调用云函数
              }, 
            }) 
          }
        }
      }) 
     
    }else
    {
      console.log("已登录");
    }
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
        this.onwedding();
      },
      fail: err => {
        console.error('[云函数] [login] 调用失败', err) 
      }
    })
  },
  onNavigateTo:function(){
    if(this.data.openid<=0)
    {
      wx.showToast({
        icon: 'none',
        title: '请授权后访问'
      })
      return;
    }
    wx.navigateTo({
      url: '/pages/photo/photo'
    })
  },

  //判断该用户是婚礼邀请函的邀请人
  onwedding:function(){  
    var that=this;
    const db = wx.cloud.database()
    db.collection('wedding_config').where({
      wedding_openid: that.data.openid
    }).get({
      success(res) {
        console.log('res.total:' + res.total)
        if (res.data.length >= 1) {
          that.setData({
            wedding_text: res.data[0].man_name+'的邀请函',
            iswedding: true,
          })
        }
      },
      fail: err => {
        console.error('[数据库] [查询记录] 失败：', err)
      }
    }) 

  },


  onNavigateToWedding: function () {
    var that = this;
    if (that.data.openid <= 0) {
      wx.showToast({
        icon: 'none',
        title: '请授权后访问'
      })
      return;
    }  
    wx.navigateTo({
      url: '/pages/wedding/wedding?wedding_openid=' + that.data.openid
    })
  },
  
  //邀请函模板1
  onNavigateToTemplet1: function () {
    var that = this;
    if (that.data.openid <= 0) {
      wx.showToast({
        icon: 'none',
        title: '请授权后访问'
      })
      return;
    }
    wx.navigateTo({
      url: "/pages/wedding/wedding?wedding_openid=templet1"
    })
  },

  //邀请函邀请人配置页
  onNavigateToWeddingConfig: function () {
    var that = this;
    if (that.data.openid <= 0) {
      wx.showToast({
        icon: 'none',
        title: '请授权后访问'
      })
      return;
    }
    wx.navigateTo({
      url: '/pages/about/about'
    })
  },

  onGotUserInfo(e) {
    this.setData({
      headurl: e.detail.userInfo.avatarUrl,
      namenick: e.detail.userInfo.nickName, 
    })   
    app.globalData.nickname = e.detail.userInfo.nickName;
    app.globalData.headurl = e.detail.userInfo.avatarUrl
    this.onGetOpenid()//调用云函数
  },
  

})