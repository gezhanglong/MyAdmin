// miniprogram/pages/csstest8/csstest8.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    animation:'',
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    var that = this;
    that.animation = wx.createAnimation({
      duration: 1000,
      timingFunction: 'linear',
      delay: 100,
      transformOrigin: 'left top 0',
    })

    that.animation.translate(150, 150).step();
    that.setData({
      animation: that.animation.export(),
    })
    var len = 150;
    setInterval(function () { 
      if(len==150){
        len=0;
      }else
      {
        len=150;
      }
      that.animation.translate(len, len).step();
      that.setData({
        animation: that.animation.export(),
      })
    }, 1200)
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
    
    const context = wx.createCanvasContext('test_ctx')
    context.setFillStyle('rgba(0,0,0,0.2)');
    context.fillRect(0,0,100,100);
    context.draw()
    context.beginPath();
    context.setFillStyle('#EEEEEE');
    context.setShadow(0, 0, 50, 'blue'); 
    context.arc(50, 50, 20, 2*Math.PI * 2)
    context.fill();
    context.draw(true); 

   
     
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