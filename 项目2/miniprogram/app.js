//app.js
App({
  onLaunch: function () {
    
    if (!wx.cloud) {
      console.error('请使用 2.2.3 或以上的基础库以使用云能力')
    } else {
      wx.cloud.init({
        traceUser: true,
      })
    } 
  },
  globalData:{ 
    openid:"",
    nickname:"",
    headurl:"",
    // weddingtype:0,//0未创建 1已提交基本资料 2已创建审核中 3审核通过 4通过不显示
    // weddingconfigId:'',//邀请函配置信息ID
  }
})
