// {
//   "navigationBarTitleText": "婚礼邀请函",
//     "disableScroll": true,
//       "navigationBarBackgroundColor": "#ff0000",
//         "backgroundTextStyle": "light"
// }
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
    openid: "",//当前用户openid
    wedding_openid: 'oeff30PoY7RSlZOPFraxExftT66A',//邀请函主人openid  
    iswedding:false,//是否已经创建，用来显示创建按钮
    wishlist: [],
    windowWidth: 0, //当前屏幕宽度
    windowHeight: 0, //当前屏幕高度  
    toView: pageList[0], //滚动到该元素  
    closemusic: 'hidden', //关闭音乐
    openmusic: '', //打开音乐
    music_move: 'animation: animation_music_move 2s linear infinite;', //打开动画
    music_src: 'https://7a6c-zltest-d3262f-1258495345.tcb.qcloud.la/陈坤-寻龙诀-(电影《寻龙诀》主题曲).mp3?sign=e79d315a1788f86d1f5ecf83fbe4f753&t=1552639934', //音乐地址
    animation_page_1_name: '', //第一屏动画
    animation_page_1_img1: '',
    animation_page_1_img2: '',
    animation_page_2_img: '', //第二屏动画
    animation_page_2_text1: '',
    animation_page_2_text2: '',
    animation_page_2_text3: '',
    animation_page_2_text4: '',
    animation_page_2_text5: '',
    animation_page_2_text6: '',
    animation_page_2_text7: '',
    animation_page_4_img: '', //第四屏动画
    page_4_click: 0, //记录点击数值
    page_4_img4: '',
    page_4_img3: '',
    page_4_img2: '',
    page_4_img1: '', 
    animation_page_5_img1: '', //第五屏动画
    animation_page_5_img3: '',
    animation_page_5_text: '',
    animation_page_7_img1: '', //第七屏动画
    animation_page_7_img2: '',
    isshowmap: 'hidden', //隐藏显示地图
    isshowbtn:'hidden',//是否显示 邀请人主任的功能按钮
    markers: [{ //地图 
      iconPath: '../../images/icon.png',
      id: 0,
      latitude: 23.089205,
      longitude: 113.309065,
      width: 60,
      height: 60,

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
        top: 600 - 100,
        width: 60,
        height: 60
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
    this.onGetOpenid() //调用云函数
  },

  onGetOpenid: function() { 
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
        if (this.data.wedding_openid == this.data.openid) {//如果打开的是邀请人主人的，就显示功能按钮 
          this.setData({
            isshowbtn: '',
          });
        } 
        this.onscrolltap(); //控制scroll上下移动方法
        this.onLoginData(); //记录登录信息
      },
      fail: err => {
        console.error('[云函数] [login] 调用失败', err)
      }
    })
  },

  // 记录登录信息
  onLoginData() {
    
    var that = this;
    const db = wx.cloud.database()
    db.collection('wedding_login').where({
      _openid: that.data.openid,
      wedding_openid:that.data.wedding_openid
    }).get({ //查询有没有这个openid
      success: res => {
        
        if (res.data.length <= 0) { 
          db.collection('wedding_login').add({ //添加数据
            data: { 
              headurl: that.data.headurl,
              wedding_openid: that.data.wedding_openid,
              nickname: that.data.nickname,
              firsttime: new Date(),
              endtime: new Date(),
              logintimes: 1,
            },
            success: function(res) {
              console.log('[数据库] [添加数据] 成功：', res)
            },
            fail: function(res) {
              console.error('[数据库] [添加数据] 失败：', res)
            }
          })
        } else {
          db.collection('wedding_login').doc(res.data[0]._id).update({ //更新数据
            data: {
              endtime: new Date(),
              logintimes: res.data[0].logintimes + 1,
            },
            success: function(res) {
              console.log('[数据库] [更新数据] 成功：', res)
            },
            fail: function(res) {
              console.error('[数据库] [更新数据] 失败：', res)
            }
          })
        }
      },
      fail: err => {
        console.error('[数据库] [查询记录] 失败：', err)
      }
    })

  },

  //跳转
  onNavigateTo: function () {  
    var that = this;
    wx.navigateTo({
      url: '/pages/weddingInfo/weddingInfo?wedding_openid=' + that.data.wedding_openid,
    })
  },


  //提交祝福语
  onSendWish: function() {
    var that = this;
    var itemList = ['祝你们百年好合，比翼双飞！', '祝你们幸福美满，早生贵子！', '祝福你们白头到老，举案齐眉!']
    wx.showActionSheet({
      itemList: itemList,
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
  onSubmit: function(wishtext) {
    var that = this;
    if (that.data.openid == '') {
      wx.showToast({
        icon: 'none',
        title: '未登陆，请先登陆'
      })
      return;
    }
    const db = wx.cloud.database()
    db.collection('wedding_wish').where({
      _openid:that.data.openid
    }).count({
      success:function(res){
        if(res.total<=0){
          db.collection('wedding_wish').add({
            data: {
              headurl: that.data.headurl,
              wedding_openid: that.data.wedding_openid,
              nickname: that.data.nickname,
              wishtext: wishtext,
              time: new Date()
            },
            success: function (res) {
              var wish = {
                headurl: that.data.headurl,
                nickname: that.data.nickname,
                wishtext: wishtext,
                wait: 0,
                move: 40,
                tops: 320
              };
              that.setData({
                wishlist: that.data.wishlist.concat(wish), //提交祝福语成功后更新页面
              });
              wx.showModal({
                title: '提示',
                content: '提交成功，感谢您的祝福!',
                showCancel: false,
              })
            }
          })
        }else{
          wx.showModal({
            title: '提示',
            content: '您的祝福已收到！',
            showCancel: false,
          })
        }
      },
      fail:function(err){
        wx.showModal({
          title: '提示',
          content: '网络开小差，请稍后提交！',
          showCancel: false,
        })
      }
    })
  
  },

  //获取祝福语
  onGetWishData: function() {
    var that = this;
    const db = wx.cloud.database()
    db.collection('wedding_wish').where({
      wedding_openid: that.data.wedding_openid,
    }).orderBy('time', 'desc').get({
      success: res => { 
        var num1 = 0,
          num2 = 0,
          num3 = 0,
          num4 = 0,
          num5 = 0; //记录每个跑道有多少个名单
        var space = 5; //动画间隔

        for (var i = 0; i < res.data.length; i++) {
          var move = 0; //运行时间
          var wait = 0; //等待等待
          var tops = Math.floor(Math.random() * 300); //设置5个跑道每个跑道60px  
          if (tops >= 0 && tops < 60) {
            move = 30;
            wait = num1 * space
            num1 = num1 + 1;
          } else if (tops >= 60 && tops < 120) {
            move = 26;
            wait = num2 * space
            num2 = num2 + 1;
          } else if (tops >= 120 && tops < 180) {
            move = 28;
            wait = num3 * space
            num3 = num3 + 1;
          } else if (tops >= 180 && tops < 240) {
            move = 20;
            wait = num4 * space
            num4 = num4 + 1;
          } else if (tops >= 240 && tops < 240) {
            move = 39;
            wait = num5 * space
            num5 = num5 + 1;
          }

          var wish = {
            headurl: res.data[i].headurl,
            nickname: res.data[i].nickname,
            wishtext: res.data[i].wishtext,
            wait: wait,
            move: move,
            tops: tops
          };
          that.setData({
            wishlist: that.data.wishlist.concat(wish),
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



  //打电话
  onPhone: function(e) {
    console.log(JSON.stringify(e))
    wx.makePhoneCall({
      phoneNumber: e._relatedInfo.anchorTargetText
    })
  },


  //音乐控制方法
  onMusic: function() {
    var that = this;
    console.log("music:" + that.data.closemusic)
    if (that.data.closemusic == 'hidden') {
      that.setData({
        closemusic: '',
        openmusic: 'hidden',
        music_move: '',
      })
      this.audioCtx.pause() //音乐暂停
    } else {
      that.setData({
        closemusic: 'hidden',
        openmusic: '',
        music_move: 'animation: animation_music_move 2s linear infinite;',
      })
      this.audioCtx.play() //音乐播放
    }
  },

  //控制scroll上下移动
  onscrolltap: function() {
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
              that.onSetOnPage_4(); 
              break;
            case 'page_5':
              that.onSetOffPage_4();
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
      setTimeout(function() { //延长3秒 防止快速点击移动
        islock = true;
      }, 100)
      console.log(that.data.toView)
    }
  },

  //切换图片
  onpageleft: function(e) {
    var that=this;
    var num = e.target.dataset.click;
    var animation = e.target.dataset.animation
    console.log("num:" + num + "," + num % 3)
    switch (num % 4) {
      case 0: 
        console.log("num:" + num + "," + num % 3 + "," + animation)
        that.setData({
          page_4_click: num + 1,
          page_4_img4: 'z-index:10',
          page_4_img3: 'z-index:10',
          page_4_img2: 'z-index:10',
          page_4_img1: 'animation: ' + animation + ' 2s linear;z-index:9',
        })
        break;
      case 1:
        that.setData({
          page_4_click: num + 1,
          page_4_img4: 'z-index:10',
          page_4_img3: 'z-index:10',
          page_4_img2: 'animation: ' + animation + ' 2s linear;z-index:8',
          page_4_img1: 'z-index:9;',
        })
        break;
      case 2:
        that.setData({
          page_4_click: num + 1, 
          page_4_img4: 'z-index:10',
          page_4_img3: 'animation: ' + animation + ' 2s linear;z-index:7;', 
          page_4_img2: 'z-index:8;',
          page_4_img1: 'z-index:9;',
        })  
        break;
      case 3:
        that.setData({
          page_4_click: num + 1,
          page_4_img4: 'animation: ' + animation + ' 2s linear;z-index:6;',
          page_4_img3: 'z-index:7;',
          page_4_img2: 'z-index:8;',
          page_4_img1: 'z-index:9;',
        })
        break;
    }

  },

  //模态窗的catchtouchmove事件，禁止模态下页面的滑动
  onCatchtouchmove: function() {},



  //初始化第一屏动画
  onSetOnPage_1: function() {
    this.setData({
      animation_page_1_name: 'animation: animation_page_1_name 2s linear;',
      animation_page_1_img1: 'animation: animation_page_1_img1 6s linear;',
      animation_page_1_img2: 'animation:animation_page_1_img1 10s, animation_page_1_img2 3s linear infinite;',
    })
  },

  //清除第一屏动画
  onSetOffPage_1: function() {
    this.setData({
      animation_page_1_name: '',
      animation_page_1_img1: '',
      animation_page_1_img2: '',
    })
  },

  //初始化第二屏动画
  onSetOnPage_2: function() {
    this.setData({
      animation_page_2_img: 'animation: animation_page_2_img 1s linear;',
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
  onSetOffPage_2: function() {
    this.setData({
      animation_page_2_img: '',
      animation_page_2_text1: '',
      animation_page_2_text2: '',
      animation_page_2_text3: '',
      animation_page_2_text4: '',
      animation_page_2_text5: '',
      animation_page_2_text6: '',
      animation_page_2_text7: '',
    })
  },

  //初始化第四屏动画
  onSetOnPage_4: function () {
    this.setData({
      page_4_img4: 'animation: animation_page_4 1s linear;z-index:10',
      page_4_img3: 'animation: animation_page_4 1.5s linear;z-index:10',
      page_4_img2: 'animation: animation_page_4 2s linear;z-index:10',
      page_4_img1: 'animation: animation_page_4 2.5s linear;z-index:10', 
    })
  },

  //清除第四屏动画
  onSetOffPage_4: function () {
    this.setData({
      page_4_img4: '',
      page_4_img3: '',
      page_4_img2: '',
      page_4_img1: '', 
    })
  },

 

  //初始化第五屏动画
  onSetOnPage_5: function() {
    this.setData({
      animation_page_5_img1: 'animation:animation_page_5_img1_1 4s linear,animation_page_5_img1_2 2s linear 4s;',
      animation_page_5_img3: 'animation:animation_page_5_text 2s linear 9s; ',
      animation_page_5_text: 'animation:animation_page_5_text 3s linear 6s;',
    })
  },

  //清除第五屏动画
  onSetOffPage_5: function() {
    this.setData({
      animation_page_5_img1: '',
      animation_page_5_img3: '',
      animation_page_5_text: '',
    })
  },

  //初始化第七屏动画
  onSetOnPage_7: function() {
    this.setData({
      animation_page_7_img1: 'animation: animation_page_7_img1 5s linear 1s;',
      animation_page_7_img2: 'animation: animation_page_7_img2 6.5s linear ; ',
    })
  },

  //清除第七屏动画
  onSetOffPage_7: function() {
    this.setData({
      animation_page_7_img1: '',
      animation_page_7_img2: '',
    })
  },


  // 获取配置信息
  onGetWeddingConfig() {
    var that = this; 
    const db = wx.cloud.database()
    db.collection('wedding_config').where({
      wedding_openid: that.data.wedding_openid,
    }).get({ 
      success: res => { 
        console.log("wedding_openid:" + JSON.stringify(res) + ";that.data.wedding_openid:" + that.data.wedding_openid)
        if(res.data.length>0){
          that.setData({
            man_name: res.data[0].man_name,
            man_phone: res.data[0].man_phone,
            page3_img1: res.data[0].page3_img1,
            page3_img2: res.data[0].page3_img2,
            page3_img3: res.data[0].page3_img3,
            page3_img4: res.data[0].page3_img4,
            page3_img5: res.data[0].page3_img5,
            page3_img6: res.data[0].page3_img6,
            page4_img1: res.data[0].page4_img1,
            page4_img2: res.data[0].page4_img2,
            page4_img3: res.data[0].page4_img3,
            page4_img4: res.data[0].page4_img4, 
            women_name: res.data[0].women_name,
            women_phone: res.data[0].women_phone,
          }); 
        }
      },
      fail: err => {
        console.error('[数据库] [查询记录] 失败：', err)
      }
    })

  },


  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function(options) {
    var that = this;
    that.setData({
      headurl: app.globalData.headurl,
      nickname: app.globalData.nickname,
      openid: app.globalData.openid,
      wedding_openid:options.wedding_openid,
      iswedding:app.globalData.iswedding,
    })
    wx.getSystemInfo({ //获取系统信息方法
      success: function(res) {
        console.log("系统信息："+JSON.stringify(res))
        that.setData({
          windowWidth: res.windowWidth,
          windowHeight: res.windowHeight,
        })
      }
    })   
    that.onGetWeddingConfig();
    that.onGetWishData();

  },


  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function() {
    this.audioCtx = wx.createAudioContext('myAudio')
   //this.audioCtx.play()//音乐播放
    this.onSetOnPage_1();
  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function() {
    this.onSetOnPage_1();
  },

  /**
   * 生命周期函数--监听页面隐藏
   */
  onHide: function() {
    this.onSetOffPage_1();
    this.onSetOffPage_2();
    this.onSetOnPage_4();
    this.onSetOffPage_5();
    this.onSetOffPage_7();
  },

  /**
   * 生命周期函数--监听页面卸载
   */
  onUnload: function() {

  },

  /**
   * 页面相关事件处理函数--监听用户下拉动作
   */
  onPullDownRefresh: function() {

  },

  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function() {

  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function(res) {
    var that = this;
    var path ='/pages/wedding/wedding';
    if (this.data.wedding_openid == this.data.openid) {//如果打开的是邀请人主人的，就显示功能按钮 
      path = '/pages/wedding/wedding?wedding_openid=' + that.data.wedding_openid;
    } 
    if (res.from === 'button') {
      console.log("来自页面内转发按钮");
      console.log(res.target);
    }
    else {
      console.log("来自右上角转发菜单")
    } 
    return {
      title: that.data.man_name + "的邀请函",
      path: path,
      imageUrl: "",  //自定义图片路径，可以是本地文件路径、代码包文件路径或者网络图片路径，支持PNG及JPG，不传入 imageUrl 则使用默认截图。显示图片长宽比是 5:4
      success: (res) => {
        console.log("转发成功", res);
      },
      fail: (res) => {
        console.log("转发失败", res);
      } 
    }
   
  }
})