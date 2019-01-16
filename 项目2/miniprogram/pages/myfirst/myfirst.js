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
    console.log(app.globalData.openid);
    if (app.globalData.openid=='')
    { 
      wx.getSetting({
        success:res=>{ 
          if (res.authSetting['scope.userInfo'])
          {
            wx.getUserInfo({
              success: res => {
                console.log(res.userInfo.nickName);
                this.setData({
                   headurl: res.userInfo.avatarUrl,
                   namenick: res.userInfo.nickName 
                   })   
              },
            })
            wx.cloud.callFunction({
              name: 'login', 
              success: res => {
                this.setData({ headurl=res.result.unionid, openid=res.result.openid })
              },
            })
          }
        }
      }) 
     
    }
  } 
})