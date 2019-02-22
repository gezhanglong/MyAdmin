// miniprogram/pages/userinfo/userinfo.js
Page({

  /**
   * 页面的初始数据
   */
  data: { 
    userlist:[],
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) { 
    var that = this;
    if (that.data.openid <= 0) {
      return;
    }
    const db = wx.cloud.database()
    db.collection('user').where({  
      userpower:1 //0最高权限，1普通权限
    }).get({
      success(res) {  //获取集合并遍历数据
      console.log("userlist:"+JSON.stringify(res));
        var fileList = [];
        for (var i = 0; i < res.data.length; i++) {
          var newarray = { openid: res.data[i]._openid, nickname: res.data[i].name};
          that.setData({
            userlist: that.data.userlist.concat(newarray),
          });
        } 
      },
      fail: err => {
        wx.showToast({
          icon: 'none',
          title: '查询记录失败'
        })
        console.error('[数据库] [查询记录] 失败：', err)
      }
    })
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