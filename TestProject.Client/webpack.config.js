const path = require('path');
var WebpackNotifierPlugin = require('webpack-notifier');

module.exports = {
	mode: 'development',
	devtool: 'source-map',
	entry: {
		index: './Scripts/src/index.js'
	},
	output: {
		filename: 'bundle.js',
		path: path.resolve(__dirname, './Scripts/build')
	},
	module: {
		rules: [
			{
				test: /\.js$/,
				exclude: /(node_modules)/,
				use: {
					loader: 'babel-loader',
					options: {
						presets: ['env', 'react', 'stage-0']
					}
				}
			}
		]
	},
	plugins: [
		new WebpackNotifierPlugin({ alwaysNotify: true })
	]
};
