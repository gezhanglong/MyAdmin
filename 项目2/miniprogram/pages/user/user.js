// miniprogram/pages/user/user.js
const app = getApp()
Page({

  /**
   * 页面的初始数据
   */
  data: { 
    headurl: "",
    nickname: "请先授权",
    openid: "",
    unionid: "",
    weddingconfig: [], 
  },

  /**
   * 生命周期函数--监听页面加载  app.globalData.openid
   */
  onLoad: function (options) { 
    var that = this;
    if (app.globalData.openid.length > 0) {
    that.setData({
      headurl: app.globalData.headurl,
      nickname: app.globalData.nickname,
      openid: app.globalData.openid, 
    }) 
      that.onwedding();
    }
  },
  

  onGotUserInfo(e) {
   
    this.setData({
      headurl: e.detail.userInfo.avatarUrl,
      nickname: e.detail.userInfo.nickName,
    }) 
    app.globalData.nickname = e.detail.userInfo.nickName;
    app.globalData.headurl = e.detail.userInfo.avatarUrl
    this.onGetOpenid()//调用云函数
  },

  //获得openid
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
        this.onlog(1);//0为我的记录 
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

  //判断该用户是婚礼邀请函的邀请人
  onwedding: function () {
    var that = this;
    const db = wx.cloud.database()
    db.collection('wedding_config').where({
      wedding_openid: that.data.openid
    }).get({
      success(res) { 
        var configList=[];
        db.collection('all_config').where({ 
        }).get({
          success(les) { 
            for (var i = 0; i < res.data.length; i++) {
              for(var j=0;j<les.data.length;i++){
                console.log("==" + res.data[i].templetId + ";" + les.data[j].templetId)
                if (res.data[i].templetId == les.data[j].templetId){ 
                  les.data[j].weddingtype = res.data[i].weddingtype;//json字符串里新增数据 
                  that.setData({
                    weddingconfig: that.data.weddingconfig.concat(les.data[j])
                  }) 
                }
              }
            }
          },
          fail: les => {
            console.error('[数据库] [查询记录] 失败：', les)
          }
        }) 
        console.log('that.data.weddingconfig:' + that.data.weddingconfig)
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
    if(e.target.dataset.weddingtype<=1){
      wx.navigateTo({
        url: "/pages/weddingconfig/weddingconfig?templetId=templet_1&horizontalNum=6&verticalNum=4"
      })
    }
    if (e.target.dataset.weddingtype==2){
      wx.navigateTo({
        url: "/pages/wedding/wedding?wedding_openid=" + that.data.openid + "&templetId=" + e.target.dataset.templetid + "&istemplet=1"
      })
    }
   
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