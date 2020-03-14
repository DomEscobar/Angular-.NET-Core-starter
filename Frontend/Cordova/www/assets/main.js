const notifier = require('node-notifier')

exports.notifier = (msg) =>  {
notifier.notify({
  title: 'AccenonGO',
  message: msg,
  wait: true
});