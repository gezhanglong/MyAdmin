const pageList = ['page_1', 'page_2', 'page_3', 'page_4', 'page_5', 'page_6', 'page_7']; 
Page({

  /**
   * 页面的初始数据
   */

  data: {
    windowWidth: 0,//当前屏幕宽度
    windowHeight: 0,//当前屏幕高度  
    toView: pageList[0],//滚动到该元素  
    animation_page_1_name:'',//第一屏动画
    animation_page_1_img1:'',
    animation_page_1_img2:'',
    animation_page_2_img: '',//第二屏动画
    animation_page_2_text1: '',
    animation_page_2_text2: '',
    animation_page_2_text3: '',
    animation_page_2_text4: '',
    animation_page_2_text5: '',
    animation_page_2_text6: '',
    animation_page_2_text7: '', 
    animation_page_5_img1: '',//第五屏动画
    animation_page_5_img3: '',
    animation_page_5_text: '', 
    animation_page_7_img1: '',//第七屏动画
    animation_page_7_img2: '', 
  },

  //控制scroll上下移动
  onscrolltap: function (event) {  
    var that=this;
    var islast=true;
    for (let i = 0; i < pageList.length-1; ++i) {
      if (pageList[i] === that.data.toView) {
        islast=false;
        that.setData({
          toView: pageList[i + 1]
        })  
        switch (that.data.toView){
          case 'page_2':
            that.onSetOnPage_2();
          break;
          case 'page_3':
            that.onSetOffPage_2();
            // that.onSetOnPage_2();
            break;
          case 'page_4':
            // that.onSetOnPage_2();
            break;
          case 'page_5':
            that.onSetOnPage_5();
            break;
          case 'page_6':
            that.onSetOffPage_5(); 
            break;
          case 'page_7':
            that.onSetOnPage_7();
            break;
        }
        that.onSetOffPage_1(); 
        break;
      }
    } 
    if(islast){ 
      that.setData({
        toView: pageList[0]
      }) 
      that.onSetOnPage_1();
      that.onSetOffPage_7(); 
    }
    console.log(that.data.toView)
  },

//模态窗的catchtouchmove事件，禁止模态下页面的滑动
 onCatchtouchmove:function(){},

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    var that = this;
    wx.getSystemInfo({//获取系统信息方法
      success: function (res) {
        that.setData({
          windowWidth: res.screenWidth,
          windowHeight: res.screenHeight,
        }) 
      }
    })
  },

  //初始化第一屏动画
  onSetOnPage_1:function(){
    this.setData({
      animation_page_1_name: 'animation: animation_page_1_name 2s linear;',
      animation_page_1_img1: 'animation: animation_page_1_img1 6s linear;',
      animation_page_1_img2: 'animation:animation_page_1_img1 10s, animation_page_1_img2 3s linear infinite;',
    })  
  },

  //清除第一屏动画
  onSetOffPage_1: function () {
    this.setData({
      animation_page_1_name: '',
      animation_page_1_img1: '',
      animation_page_1_img2: '',
    })  
  },

  //初始化第二屏动画
  onSetOnPage_2: function () {
    this.setData({
      animation_page_2_img:'animation: animation_page_2_img 1s linear;',
      animation_page_2_text1: 'animation:animation_page_2 12s,  animation_page_2_text 12s linear ;',
      animation_page_2_text2: 'animation:animation_page_2 12s,  animation_page_2_text 12s linear  2s;',
      animation_page_2_text3: 'animation:animation_page_2 12s,  animation_page_2_text 12s linear  4s;',
      animation_page_2_text4: 'animation:animation_page_2 12s,  animation_page_2_text 12s linear  6s;',
      animation_page_2_text5: 'animation:animation_page_2 12s,  animation_page_2_text 12s linear  8s;',
      animation_page_2_text6: 'animation:animation_page_2 12s,  animation_page_2_text 12s linear  10s;',
      animation_page_2_text7: 'animation:animation_page_2 12s,  animation_page_2_text 12s linear  12s;', 
    })
  },

  //清除第二屏动画
  onSetOffPage_2: function () {
    this.setData({
      animation_page_2_img:'',
      animation_page_2_text1: '',
      animation_page_2_text2: '',
      animation_page_2_text3: '',
      animation_page_2_text4: '',
      animation_page_2_text5: '',
      animation_page_2_text6: '',
      animation_page_2_text7: '', 
    })
  },

  //初始化第五屏动画
  onSetOnPage_5: function () {
    this.setData({
      animation_page_5_img1: 'animation:animation_page_5_img1_1 4s linear,animation_page_5_img1_2 2s linear 4s;',
      animation_page_5_img3: 'animation:animation_page_5_text 2s linear 9s; ',
      animation_page_5_text: 'animation:animation_page_5_text 3s linear 6s;', 
    })
  },

  //清除第五屏动画
  onSetOffPage_5: function () {
    this.setData({
      animation_page_5_img1: '',
      animation_page_5_img3: '',
      animation_page_5_text: '', 
    })
  },

  //初始化第七屏动画
  onSetOnPage_7: function () {
    this.setData({
      animation_page_7_img1: 'animation: animation_page_7_img1 5s linear 1s;',
      animation_page_7_img2: 'animation: animation_page_7_img2 6.5s linear ; ', 
    })
  },

  //清除第七屏动画
  onSetOffPage_7: function () {
    this.setData({
      animation_page_7_img1: '',
      animation_page_7_img2: '', 
    })
  },



  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function () {
    this.onSetOnPage_1();
  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function () {
    this.onSetOnPage_1();
  },

  /**
   * 生命周期函数--监听页面隐藏
   */
  onHide: function () {
    this.onSetOffPage_1();
    this.onSetOffPage_2();
    this.onSetOffPage_5();
    this.onSetOffPage_7(); 
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