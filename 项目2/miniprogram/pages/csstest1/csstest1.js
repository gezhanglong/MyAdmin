// miniprogram/pages/csstest1/csstest1.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    btnclass:'box-bnt',//样式
    setInter:'',//定时器句柄  
    num:0,//滚动位置
    scrollleft: 0, //滚动条位置
    windowWidth:0,//当前分辨率宽度
    windowHeight:0,//当前分辨率高度
    guessWidth: 0,//‘猜’当前宽度
    guessHeight: 0,//‘猜’当前高度
    guesssetInter: '',//‘猜’定时器句柄
    guessopacity:1.0,//'猜'透明度
  },
 //控制css样式
  onBtnstart:function(){
    this.setData({ 
      btnclass:'box-bnt-on',
    })
  },
  onBtnend: function () {
    this.setData({
      btnclass: 'box-bnt',
    })
  },


  //定时器 轮播图
  startSetInter: function(){
      var that = this; 
      that.data.setInter = setInterval(
          function () {
              var numVal = that.data.num -1;
              if(numVal>-2250)
              {
                that.setData({ num: numVal });
                console.log(that.data.num);
              }else
              {
                that.setData({ num:750});
                console.log(that.data.num);
              }
              
          }
    , 20);   
  },

  
  //定时器 轮播图
  onCrolltolower:function(){
    var that = this;
    var scrollleft = that.data.scrollleft;
    that.data.setInter = setInterval(
      function () {
        var numVal = that.data.scrollleft + 750;
        if (numVal < 2250) {
          that.setData({ scrollleft: numVal });
          console.log("触发1" +that.data.scrollleft);
        } else {
          that.setData({ scrollleft: 0 });
          console.log("触发2" +that.data.scrollleft);
        }

      }
      , 3000);   
  },

  //“猜”定时器
  onGuessSetInter: function () {
    var that = this;
    that.data.guesssetInter = setInterval(
      function () {
        var width = Math.floor(Math.random() * that.data.windowWidth);
        var height = Math.floor(Math.random() * that.data.windowHeight); 
        for(var i=0;i<5;i++){
          setTimeout(function () { 
          }, 5000)  
          that.setData({ guessopacity: 1 - parseFloat(i / 10) });
          console.log("透明度：" + that.data.guessopacity); 
        }
       
        that.setData({ guessWidth: width, guessHeight: height, guessopacity:1.0});
        console.log("猜：" + width + ";" + height); 
      }
      , 20000);
  },

  //预览图片
  onPreviewImage: function (e) {
    var current = e.target.dataset.src;
    wx.previewImage({
      current: current, // 当前显示图片的http链接
      urls: [current] // 需要预览的图片http链接列表
    })
  },


  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
      //  this.startSetInter();//轮播方法
    this.onCrolltolower();//轮播方法
    this.onGuessSetInter();//'猜'定时器
    var that = this;
    wx.getSystemInfo({//获取系统信息方法
      success: function (res) { 
        that.setData({
          windowWidth: res.windowWidth * res.pixelRatio,
          windowHeight: res.windowHeight * res.pixelRatio
        })
        console.log("cells:" + JSON.stringify(res));
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