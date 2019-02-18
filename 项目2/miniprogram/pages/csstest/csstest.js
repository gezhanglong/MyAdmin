// miniprogram/pages/csstest/csstest.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    hide1:'hide',
    hide2: 'hide',
    hide3: 'hide',
    backgroundcolor:'',
    scrollleft:0,  
    color1:'#4d4',
    color2: '#4dd',
    color3: '#4dd',
  }, 
  onScroll:function(){ 
    var scrollleft=0;
    if (this.data.scrollleft + 300 >= 900 ) {
      scrollleft=0;
    }else{
      scrollleft=this.data.scrollleft + 300
    }
    switch(scrollleft){
      case 0:
        this.setData({
          scrollleft: scrollleft,
          color1: '#4d4',
          color2: '#4dd',
          color3: '#4dd',
        })
      break;
      case 300: 
      this.setData({
        scrollleft: scrollleft,
        color1: '#4dd',
        color2: '#4d4',
        color3: '#4dd',
      })
        break;
      case 600:
        this.setData({
          scrollleft: scrollleft,
          color1: '#4dd',
          color2: '#4dd',
          color3: '#4d4',
        })
        break;
    } 
    console.log(this.data.scrollleft);
  },
 

 ontap1:function(){ 
   this.setData({
     hide1:'',
     hide2: 'hide',
     hide3: 'hide',
     backgroundcolor:'red'
   }) 
 }, 
  ontap2: function () { 
    this.setData({
      hide1: 'hide',
      hide2: '',
      hide3: 'hide',
      backgroundcolor: 'yellow'
    })
  },
  ontap3: function () { 
    this.setData({
      hide1: 'hide',
      hide2: 'hide',
      hide3: '',
      backgroundcolor: 'black'
    })
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) { 
   
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