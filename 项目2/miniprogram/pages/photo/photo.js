// miniprogram/pages/photo/photo.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    imgurls:[]
    // imgurls: [{ url: 'https://7a6c-zltest-d3262f-1258495345.tcb.qcloud.la/my-image.jpg?sign=416e7dcc678ea18f2ec07a48705c3e9a&t=1550115920', name: '我的图片', date: '2019-2-14' }, { url: 'https://7a6c-zltest-d3262f-1258495345.tcb.qcloud.la/1533260567578-3537830.jpg?sign=bb04f28bd3a79e9195733f9d005ba17f&t=1550129391', name: '我的图片', date: '2019-2-14' }, { url: 'https://7a6c-zltest-d3262f-1258495345.tcb.qcloud.la/my-image.png?sign=dc07f0e71bfec5e95d3f242ff4349367&t=1550129407', name: '我的图片', date: '2019-2-14' }],
  },

  //预览图片
  onPreviewImage:function(e){
    var current= e.target.dataset.src;
    wx.previewImage({
      current: current, // 当前显示图片的http链接
      urls: [current] // 需要预览的图片http链接列表
    })
  },
 

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) { 
    var that = this;
    const db = wx.cloud.database()
    const photoCollection = db.collection('photoinfo').get({
      success(res) {  //获取集合并遍历数据
        var fileList=[];
        for(var i=0;i<res.data.length;i++)
        { 
          fileList.push(res.data[i].imgurl);
        }
        console.log("res:" +JSON.stringify(res.data));
        console.log("fileList:" + fileList);
        if(fileList.length>0)
        {
          wx.cloud.getTempFileURL({//下载图片 获得图片地址  
            fileList: fileList,
            success: function (des) {
              console.log("下载成功：" +JSON.stringify(des.fileList));
              for (var j = 0; j < des.fileList.length; j++) {
                for (var i = 0; i < res.data.length; i++) {
                  if (des.fileList[j].fileID == res.data[i].imgurl) {
                    var newarray = { url: des.fileList[j].tempFileURL, name: res.data[i].des, date: res.data[i].time.getFullYear() + "-" + (res.data[i].time.getMonth() + 1) + "-" + res.data[i].time.getDate() + " " + res.data[i].time.getHours() + ":" + res.data[i].time.getMinutes() + ":" + res.data[i].time.getSeconds() };
                    that.setData({
                      imgurls: that.data.imgurls.concat(newarray),
                    });
                  }
                }

              }

            }
          }) 
        }
       
      },
      fail(res){
        console.log("集合错误：" + res);
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