// miniprogram/pages/csstest2/csstest2.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    imgurls: [],
    setInter:'',
    internum:0,//定时器循环多少次
    imgcontent0: '',
    transform0:'  ',
    imgcontent1: '',
    transform1: ' ',
    imgcontent2: '',
    transform2: ' ',
    imgcontent3: '',
    imgcontent4: '',
    imgcontent5: '',
    imgcontent6: '',
    imgcontent7: '',
  },

  // onBindtap:function(){  
  //   this.setData({
  //     transform0: 'transform:rotate(7deg);',
  //   });
  // },


  //定时器
  startSetInter: function () {
    var that = this;
    that.data.setInter = setInterval(
      function () {
        if(that.data.internum==0){
          that.setData({
            imgcontent0: that.data.imgurls[0].url,
            transform0: 'animation: myfirst2 2s linear, myfirst1 2s linear 2s ;',
            internum: 1, 
          }); 
        } else
        if (that.data.internum == 1) {
          that.setData({
             
            internum: 2,
            imgcontent1: that.data.imgurls[1].url,
            transform1: 'animation: myfirst2 2s linear, myfirst1 2s linear 2s ;', 
          }); 
        } else
        if (that.data.internum == 2) {
          that.setData({ 
            internum: 3, 
            imgcontent2: that.data.imgurls[2].url,
            transform2: 'animation: myfirst2 2s linear, myfirst1 2s linear 2s ;',
          }); 
        }  
         else{
          clearInterval(that.data.setInter); 
         }
        console.log("定时器" + that.data.internum);

      }
      , 2000);
  },

  // 获取配置信息
  onGetWeddingConfig() {
    var that = this;
    const db = wx.cloud.database()
    db.collection('wedding_config').where({
      wedding_openid: 'oeff30PoY7RSlZOPFraxExftT66A',
    }).get({
      success: res => {
        console.log("wedding_openid:" + JSON.stringify(res) + ";that.data.wedding_openid:" + that.data.wedding_openid)
        if (res.data.length > 0) {
          wx.cloud.getTempFileURL({
            fileList: res.data[0].page3_six,
            success: res => { 
              console.log(res.fileList)
            },
            fail: err => {
              console.log(JSON.stringify(err))
            }
          })
          // that.setData({
          //   man_name: res.data[0].man_name,
          //   man_phone: res.data[0].man_phone,
          //   page3_img1: res.data[0].page3_img1,
          //   page3_img2: res.data[0].page3_img2,
          //   page3_img3: res.data[0].page3_img3,
          //   page3_img4: res.data[0].page3_img4,
          //   page3_img5: res.data[0].page3_img5,
          //   page3_img6: res.data[0].page3_img6,
          //   page4_img1: res.data[0].page4_img1,
          //   page4_img2: res.data[0].page4_img2,
          //   page4_img3: res.data[0].page4_img3,
          //   page4_img4: res.data[0].page4_img4,
          //   women_name: res.data[0].women_name,
          //   women_phone: res.data[0].women_phone,
          // });
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
  onLoad: function (options) {
    this.onGetWeddingConfig();
    // var textarray=[];
    // var item='clud:xxxx';
    // var item1 = 'clud:xxxx';
    // textarray.push(item);
    // textarray.push(item1);
    // console.log("textarray:"+JSON.stringify(textarray));
     
    // var that = this; 
    // const db = wx.cloud.database()
    // const photoCollection = db.collection('photoinfo').get({
    //   success(res) {  //获取集合并遍历数据
    //     var fileList = [];
    //     for (var i = 0; i < res.data.length; i++) {
    //       fileList.push(res.data[i].imgurl);
    //     }
    //     console.log("res:" + JSON.stringify(res.data));
    //     console.log("fileList:" + fileList);
    //     if (fileList.length > 0) {
    //       wx.cloud.getTempFileURL({//下载图片 获得图片地址  
    //         fileList: fileList,
    //         success: function (des) {
    //           console.log("下载成功：" + JSON.stringify(des.fileList));
    //           for (var j = 0; j < des.fileList.length; j++) {
    //             for (var i = 0; i < res.data.length; i++) {
    //               if (des.fileList[j].fileID == res.data[i].imgurl) {
    //                 var newarray = { url: des.fileList[j].tempFileURL, name: res.data[i].des, date: res.data[i].time.getFullYear() + "-" + (res.data[i].time.getMonth() + 1) + "-" + res.data[i].time.getDate() + " " + res.data[i].time.getHours() + ":" + res.data[i].time.getMinutes() + ":" + res.data[i].time.getSeconds() };
    //                 that.setData({
    //                   imgurls: that.data.imgurls.concat(newarray),   
    //                 }); 
    //               }
    //             }

    //           }
    //          that.startSetInter();
    //         }
    //       })
    //     }

    //   },
    //   fail(res) {
    //     console.log("集合错误：" + res);
    //   }
    // }) 
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