// miniprogram/pages/weddingconfig/weddingconfig.js
const app = getApp();
var islock=true;
Page({

  /**
   * 页面的初始数据
   */
  data: {
    tempImages:[],//选择图片预览图url
    hidebase:'',//隐藏 基本资料tab
    hideimage:'hidden'//隐藏 图片资料tab
  },

  onshowToast:function(text){
    wx.showToast({
      title: text,
      duration: 2000,
      icon: "none"
    })
   islock=true;
  },

  //提交数据
  onSubmit: function (e) {
    var that = this;  
    let str = /^1\d{10}$/; 
    if(islock){
      islock=false;
      if ((e.detail.value.man_name.length < 2 || e.detail.value.man_name.length > 4)) {
        that.onshowToast('姓名1长度错误');
        return;
      }
      if (!str.test(e.detail.value.man_phone)) {
        that.onshowToast('手机号1格式错误');
        return;
      }
      if ((e.detail.value.women_name.length < 2 || e.detail.value.women_name.length > 4)) {
        that.onshowToast('姓名2长度错误');
        return;
      }

      if (!str.test(e.detail.value.women_phone)) {
        that.onshowToast('手机号2格式错误');
        return;
      }

      if (app.globalData.openid.length < 1) {
        if (e.detail.value.address.length < 10) {
          that.onshowToast('未授权，无法提交');
          return;
        }
      }
      if (e.detail.value.address.length < 10) {
        that.onshowToast('宴客地址不能为空或者不够详细！');
        return;
      }

      wx.showModal({
        title: '提示',
        content: '提交成功后，修改资料请联系管理员，是否需要提交？',
        showCancel: true,
        success(res) {
          if (res.confirm) {
            const db = wx.cloud.database()
            db.collection('wedding_config').add({
              data: {
                address: e.detail.value.address,
                man_name: e.detail.value.man_name,
                man_phone: e.detail.value.man_phone,
                page3_img1: '',
                page3_img2: '',
                age3_img3: '',
                page3_img4: '',
                page3_img5: '',
                page3_img6: '',
                page4_img1: '',
                page4_img2: '',
                wedding_openid: app.globalData.openid,
                women_name: e.detail.value.women_name,
                women_phone: e.detail.value.women_phone,
                time: new Date()
              },
              success: function (res) {
                that.onshowToast('提交成功');
                setTimeout(function () {
                  wx.navigateTo({
                    url: "/pages/weddingabout/weddingabout"
                  })
                }, 2000)
              },
              fail: function () {
                that.onshowToast('系统繁忙请稍后提交或联系管理员');
              }
            })
          } else if (res.cancel) {

          }
        }
      })

    }

  },

  onTab:function(){
    this.setData({
      hideimage: '',
      hidebase: 'hidden',
    })
  },

  //选择图片
  onChooseImage: function () {
    var that = this; 
    wx.chooseImage({//选择图片
      count: 3,
      sizeType: ['original', 'compressed'],
      sourceType: ['album', 'camera'],
      success: function (res) {
        console.log(JSON.stringify(res));
        for(var i=0;i<res.tempFilePaths.length;i++){
          var filePath = res.tempFilePaths[i];
          var newarray = { url: filePath};
          that.setData({
            tempImages: that.data.tempImages.concat(newarray), 
          }); 
        } 
      }
    })
  },

  //上传图片并下载图片
  onUpdateimg:function(){

    // const cloudPath = app.globalData.openid + "/" + Date.parse(new Date()) + filePath.match(/\.[^.]+?$/)[0];
    // wx.cloud.uploadFile({//上传图片
    //   cloudPath,
    //   filePath,
    //   header: {
    //     "Content-Type": "multipart/form-data"
    //   },
    //   success: function (res) {
    //     const cloudfileid = res.fileID;
    //     that.setData({
    //       imgurl: filePath,
    //       fileid: cloudfileid,
    //       previewimgurl: filePath,
    //     }),
    //       console.log("上传成功：" + cloudfileid);
    //     wx.cloud.downloadFile({//下载图片 获得图片地址  
    //       fileID: that.data.fileid,
    //       success: function (res) {
    //         console.log("下载成功：" + res.tempFilePath),
    //           that.setData({
    //             cloudimgurl: res.tempFilePath,
    //           })
    //       }
    //     })
    //   },
    //   fail: function (res) {
    //     console.log("上传失败：" + res.errMsg + ";code:" + res.errCode)
    //   }
    // })

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