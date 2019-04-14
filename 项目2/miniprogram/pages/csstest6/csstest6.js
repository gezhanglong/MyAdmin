// miniprogram/pages/csstest6/csstest6.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    isnight: true,//true为晚上；false为白天
    moon: 0,//从0到105为半个月，一天7格
    suntop: 0,//太阳的top位置 从120到30 1个小时15（除于6）
    sunleft: 0,//太阳的left位置 从-150到700 1个小时间隔71
  },

  //计算晚上月亮的角度，白天太阳位置
  oncount: function () {
    var that = this;
    var today = new Date();
    var day = today.getDate();
    var hour = today.getHours();
    if (hour >= 6 && hour < 18) {//白天
      var sunleft=-150+(hour-6)*71;
      var suntop=0;
      if(hour<=12){
        suntop=120 - (hour - 6) * 15
      }else{
        suntop = (hour - 6) * 15
      }
      that.setData({
        isnight: false,
        sunleft:sunleft,
        suntop:suntop,
      })
    } else {
      var moon = day * 7;
      if (moon > 105) {
        moon = 105 - (day - 15) * 7;
      }
      that.setData({
        isnight: true,
        moon: moon,
      })
     
    }
    console.log("moon:" + that.data.moon + ",day:" + day + ",hour:" + hour)
  },

  ontest:function(){  
      var that = this; 
      var day =14;
      var hour = 18;
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
      } else {
        var moon = day * 7;
        if (moon > 105) {
          moon = 105 - (day - 15) * 7;
        }
        that.setData({
          isnight: true,
          moon: moon,
        })

      }
    
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    //this.oncount();
    this.ontest();
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