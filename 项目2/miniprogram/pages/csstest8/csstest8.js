// miniprogram/pages/csstest8/csstest8.js
const context = wx.createCanvasContext('test_ctx');
var emitter=0;
var color=0;
Page({

  /**
   * 页面的初始数据
   */
  data: {
    animation:'',
    w:0,
    h:0,
    dots:[],
    cx:0,
    cy:0,
    controls:{
      maxDots :10,
      maxSpeed: 3,
      minSpeed: 1,
      emitRate: 10,
      emitNum: 2,
      radius: 2,
      trail: 0.2,
      maxTime: 6000,
      minTime: 1000,
      glow: 0, 
      redraw: function () {
       
      },
    },
  },
 

  emitDots: function (){
    
    var that =this; 
    if (that.data.dots.length < that.data.controls.maxDots) {
      for (var i = 0; i < that.data.controls.emitNum; i++) {
       
        var col = Math.random() >= 0.5;
        col ? color = 35 : color = 180;
        that.data.dots.push({
          x: that.data.cx,
          y: that.data.cy,
          v: Math.random() * (that.data.controls.maxSpeed - that.data.controls.minSpeed) + that.data.controls.minSpeed,
          d: Math.random() * 360,
          c: Math.random() * (5 - (-5)) + (-5),
          h: color,
          st: Date.now(),
          lt: Math.random() * (that.data.controls.maxTime - that.data.controls.minTime) + that.data.controls.minTime
        });
      }
    } 
    console.log("emitDots："+JSON.stringify(that.data.dots));
  },


  draw :function (){
    var that=this;  
    context.setFillStyle("rgba(0,5,0," + that.data.controls.trail+")");
    context.fillRect(0, 0, that.data.w, that.data.h);
    context.draw()
    for (var i = 0; i < that.data.dots.length; i++){
      var pct = (Date.now() - that.data.dots[i].st) / that.data.dots[i].lt;
  
      context.beginPath();
      context.setFillStyle("rgba(" + that.data.dots[i].h+", 50, 153," + (1 - pct) + ")");
      context.setShadow(0, 0, 50,"rgba(" + that.data.dots[i].h+", 50, 153,1)");
      context.arc(that.data.dots[i].x, that.data.dots[i].y, Math.pow(that.data.controls.radius, 2) / that.data.dots[i].v, 0, Math.PI * 2)
      context.fill();
      context.draw(true); 


      var myx = that.data.dots[i].x + that.data.dots[i].v * Math.cos(that.data.dots[i].d * Math.PI / 180);
      var myy= that.data.dots[i].y + that.data.dots[i].v * Math.sin(that.data.dots[i].d * Math.PI / 180);
      var myd = that.data.dots[i].d + that.data.dots[i].c;

      
      this.setData({
        [`dots[${i}].x`]: myx,
        [`dots[${i}].y`]: myy,
        [`dots[${i}].d`]: myd,
      }) 
      if (that.data.dots[i].x > that.data.w || that.data.dots[i].x < 0 || that.data.dots[i].y > that.data.h || that.data.dots[i].y < 0 || that.data.dots[i].st + that.data.dots[i].lt < Date.now()) {
         
      that.data.dots.splice(i, 1)
  }
    
  }
    console.log("end_emitDots：" + JSON.stringify(that.data.dots));
    this.draw();
},

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    var that = this;
    wx.getSystemInfo({//获取系统信息方法
      success: function (res) {
        that.setData({
          w: res.windowWidth ,
          h: res.windowHeight ,
          cx: (res.windowWidth )/2,
          cy: (res.windowHeight )/2,
        })
        console.log("cells:" + JSON.stringify(res)); 
      }
    })
     
     //动画
    // that.animation = wx.createAnimation({
    //   duration: 1000,
    //   timingFunction: 'linear',
    //   delay: 100,
    //   transformOrigin: 'left top 0',
    // })

    // that.animation.translate(150, 150).step();
    // that.setData({
    //   animation: that.animation.export(),
    // })
    // var len = 150;
    // setInterval(function () { 
    //   if(len==150){
    //     len=0;
    //   }else
    //   {
    //     len=150;
    //   }
    //   that.animation.translate(len, len).step();
    //   that.setData({
    //     animation: that.animation.export(),
    //   })
    // }, 1200)
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
  //   clearInterval(emitter)
  //   emitter = setInterval(this.emitDots, this.data.controls.emitRate); 
  // this.draw();
    // setInterval(this.draw, 100);  
    
    
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