
const app = getApp();
const pageList = ['page_1', 'page_2', 'page_3', 'page_4', 'page_5', 'page_6', 'page_7'];
var islock = true; //防止快速点击开关

Page({

  /**
   * 页面的初始数据
   */

  data: {
    headurl: "",
    nickname: "",
    openid: "", 
    windowWidth: 0,//当前屏幕宽度
    windowHeight: 0,//当前屏幕高度  
    toView: pageList[0],//滚动到该元素  
    closemusic:'hidden',//关闭音乐
    openmusic:'',//打开音乐
    music_move: 'animation: animation_music_move 2s linear infinite;',//打开动画
    music_src: 'https://7a6c-zltest-d3262f-1258495345.tcb.qcloud.la/陈坤-寻龙诀-(电影《寻龙诀》主题曲).mp3?sign=e79d315a1788f86d1f5ecf83fbe4f753&t=1552639934',//音乐地址
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
    isshowmap:'hidden',//隐藏显示地图
    markers: [{//地图 
      iconPath: '../../images/icon.png',
      id: 0,
      latitude: 23.089205,
      longitude: 113.309065,
      width: 30,
      height: 30,
      
      callout: {
        content: "6月1号(星期六)下午6点半晚宴\n期待您的到来",
        color: "#000000",
        fontSize: 18,
        borderRadius: 10,
        bgColor: "#F8F8F8",
        display: "ALWAYS",
        boxShadow: "2px 2px 10px #aaa"
      },
      label: {
        color: "#000",
        fontSize: 12,
        content: "设宴广州酒家贵宾厅",
        x: 23.089205,
        y: 113.309065
      } 
    }],
    polyline: [{
      points: [{
        longitude: 113.309065,
        latitude: 23.089205
      }, {
          longitude: 113.309065,
          latitude: 23.089205
      }],
      color: '#FF0000DD',
      width: 2,
      dottedLine: true
    }],
    controls: [{
      id: 1,
      iconPath: '../../images/icon.png',
      position: {
        left: 0,
        top: 300 - 50,
        width: 30,
        height: 30
      },
      clickable: true
    }],
  },

  //授权
  onGetUserInfo(e) {
    this.setData({
      headurl: e.detail.userInfo.avatarUrl,
      nickname: e.detail.userInfo.nickName,
    })
    app.globalData.nickname = e.detail.userInfo.nickName;
    app.globalData.headurl = e.detail.userInfo.avatarUrl
    this.onGetOpenid()//调用云函数
  },

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
        this.onscrolltap(); //控制scroll上下移动
      },
      fail: err => {
        console.error('[云函数] [login] 调用失败', err)
      }
    })
  },


  //提交祝福语
  onSendWish:function(){
    var that=this;
    var itemList = ['恭喜恭喜', '恭喜发财', '财源滚滚']
    wx.showActionSheet({
      itemList: itemList ,
      success(res) {  
        that.onSubmit(itemList[res.tapIndex]) 
        console.log(itemList[res.tapIndex])
      },
      fail(res) {
        console.log(res.errMsg)
      }
    })
  },

    //提交数据
  onSubmit: function (wishtext) {
    console.log("1/"+wishtext)
    var that = this; 
    if (that.data.openid =='') {
      wx.showToast({
        icon: 'none',
        title: '未登陆，请先登陆'
      })
      return;
    }
    console.log("2/" + wishtext)
    const db = wx.cloud.database()
    db.collection('wedding-zl').add({
      data: {
        headurl: that.data.headurl, 
        openid: that.data.openid, 
        nickname: that.data.nickname, 
        wishtext: wishtext,
        time: new Date()
      },
      success: function (res) {
        wx.showModal({
          title: '提示',
          content: '提交成功，感谢您的祝福！',
          showCancel: false, 
        })
      }
    })
  },

  //打电话
  onPhone:function(e){
    console.log(JSON.stringify(e))
    wx.makePhoneCall({
      phoneNumber: e._relatedInfo.anchorTargetText  
    })
  },


  //音乐控制方法
  onMusic:function(){
    var that = this;
    console.log("music:" + that.data.closemusic )
    if (that.data.closemusic =='hidden'){
      that.setData({
        closemusic: '',
        openmusic: 'hidden',
        music_move:'',
      })
      this.audioCtx.pause()//音乐暂停
    }else {
      that.setData({
        closemusic: 'hidden',
        openmusic: '',
        music_move: 'animation: animation_music_move 2s linear infinite;',
      })
      this.audioCtx.play()//音乐播放
    }
  },

  //控制scroll上下移动
  onscrolltap: function () {
    if (islock) {
      islock = false; 
      var that = this;
      var islast = true;
      for (let i = 0; i < pageList.length - 1; ++i) {
        if (pageList[i] === that.data.toView) {
          islast = false;
          that.setData({
            toView: pageList[i + 1]
          })
          switch (that.data.toView) {
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
              that.setData({
                isshowmap: '',
              })
              that.onSetOffPage_5();
              break;
            case 'page_7':
              that.setData({
                isshowmap: 'hidden',
              })
              that.onSetOnPage_7();
              break;
          }
          that.onSetOffPage_1();
          break;
        }
      }
      if (islast) {
        that.setData({
          toView: pageList[0]
        })
        that.onSetOnPage_1();
        that.onSetOffPage_7();
      }
      setTimeout(function () {//延长3秒 防止快速点击移动
        islock=true;
      }, 3000)  
      console.log(that.data.toView)
    }
  },

//模态窗的catchtouchmove事件，禁止模态下页面的滑动
 onCatchtouchmove:function(){},

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    var that = this;
    that.setData({
      headurl: app.globalData.headurl,
      nickname: app.globalData.nickname,
      openid: app.globalData.openid,  
    })
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
    this.audioCtx = wx.createAudioContext('myAudio')
    this.audioCtx.play()//音乐播放
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