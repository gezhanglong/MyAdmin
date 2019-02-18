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
    unionid:""
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
  onGotUserInfo(e) {
    this.setData({
      headurl: e.detail.userInfo.avatarUrl,
      namenick: e.detail.userInfo.nickName, 
    })   
    app.globalData.nickname = e.detail.userInfo.nickName;
    this.onGetOpenid()//调用云函数
  },
  

})