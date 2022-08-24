<template>
	<view class="page_edu">

		<view class="page_edu_header">
			<view class="header">
				<navigator open-type="switchTab" url="/pages/register/register">
					<image src="/static/icon_main.png" class="btn"></image>
				</navigator>
				<view class="input">
					<image src="/static/search.png" class="search"></image>
					<input type="text" value="" placeholder="搜索" />
				</view>
				<!-- <image src="/static/msg.png" class="btn"></image> -->
			</view>
			<view class="header_content">
				<view class="left">
					<text class="title">闽西职业技术学院</text>
					<text class="title" style="font-size: 1.5rem;margin-top: 3%;">教务管理系统</text>
				</view>
				<!-- <view>
					<image src="/static/right.png" style="width: 131px;height: 122px;"></image>
				</view> -->
			</view>
		</view>
		<view class="page_content">
			<view class="menu">
				<template v-for="(it,i) in menus">
					<view class="item" :key="'menu_'+i">
						<text class="txt">{{it.txt}}</text>
						<view class="img_view" :style="{background: it.bg}">
							<image :src="it.icon" class="image"></image>
						</view>
					</view>
				</template>
			</view>
			<view class="ad">
				<view class="ad_btn">
					<text class="title">校园风采</text>
				</view>
			</view>
			<view class="s_menu">
				<text class="title">xxxxxxxxx</text>
				<text class="title">xxxxxxxxx</text>
			</view>
			<view class="s_menu">
				<text class="title">xxxxxxxxx</text>
				<text class="title">xxxxxxxxx</text>
			</view>
			<view class="ad">
				<view class="ad_btn">
					<text class="title">校园公告栏</text>
				</view>
			</view>
			<view class="s_menu">
				<text class="title">xxxxxxxxx</text>
				<text class="title">xxxxxxxxx</text>
			</view>
			<view class="s_menu">
				<text class="title">xxxxxxxxx</text>
				<text class="title">xxxxxxxxx</text>
			</view>
		</view>
	</view>
</template>

<script>
	export default {
		data() {
			return {
				menus: [{
						bg: 'linear-gradient(0deg,rgba(9,216,162,1),rgba(90,242,217,1))',
						icon: '/static/graduation.png',
						txt: '成绩查询',
						isFree: true
					},
					{
						bg: 'linear-gradient(0deg,rgba(251,184,35,1),rgba(255,228,40,1))',
						icon: '/static/live.png',
						txt: '信息填报',
						isFree: true
					},
					{
						bg: 'linear-gradient(0deg,rgba(255,126,34,1),rgba(240,184,27,1))',
						icon: '/static/emblem.png',
						txt: '素质证书',
						isFree: true
					},
					{
						bg: 'linear-gradient(0deg,rgba(9,177,252,1),rgba(24,200,255,1))',
						icon: '/static/question_bank.png',
						txt: '反馈问题',
						isFree: true
					}
				],
				textList: '网站3.30日22：00-24：00将进行更新，将导致app不能使用，此时间段内请不要访问'

			}
		},
		onLoad() {

		},
		methods: {

		}
	}
</script>

<style>
	page {
		width: 100%;
		background-color: #ebebeb;
	}
</style>

<style lang="scss" scoped>
	@function realSize($args) {
		@return $args / 1.5;
	}

	.page_edu {
		width: 100%;
	}

	.page_edu_header {
		padding-top: var(--status-bar-height);
		background-image: linear-gradient(to top, #48c6ef 0%, #6f86d6 100%);
		width: 100%;
		height: realSize(300px);

		.header {
			display: flex;
			flex-direction: row;
			align-items: center;
			padding: realSize(20px);

			.btn {
				width: realSize(30px);
				height: realSize(30px);
			}

			.input {
				height: realSize(59px);
				width: 100%;
				margin-left: realSize(20px);
				// margin-right: realSize(5px);
				background: rgba(255, 255, 255, 1);
				border-radius: realSize(30px);
				display: flex;
				flex-direction: row;
				align-items: center;

				.search {
					width: realSize(30px);
					height: realSize(30px);
					margin-left: realSize(20px);
					margin-right: realSize(20px);
				}
			}
		}

		.header_content {
			display: flex;
			flex-direction: row;

			.left {
				display: flex;
				flex-direction: column;
				width: 57%;
				margin-top: 10px;
				margin-left: 15px;
				margin-right: 15px;

				.title {
					width: realSize(419px);
					height: realSize(59px);
					font-size: realSize(47px);
					font-weight: bold;
					color: rgba(255, 255, 255, 1);
				}

				.sub_title {
					margin-top: 3px;
					font-size: realSize(18px);
					font-weight: 400;
					color: rgba(255, 255, 255, 1);

					background: linear-gradient(0deg, rgba(120, 255, 224, 1) 0%, rgba(255, 255, 255, 1) 100%);
					-webkit-background-clip: text;
					-webkit-text-fill-color: transparent;
				}


			}
		}
	}

	.page_content {
		width: 100%;
		margin-top: -39.5px;

		.menu {
			margin-left: 10px;
			margin-right: 10px;
			padding-left: 15px;
			padding-right: 15px;
			height: realSize(176px);
			// background: rgba(255, 255, 255, 1);
			// background-image: linear-gradient(to top, #9890e3 0%, #b1f4cf 100%);
			box-shadow: 0px 10px 10px 0px rgba(0, 161, 124, 0.1);
			border-radius: 10px;
			display: flex;
			flex-direction: row;
			align-items: stretch;
			justify-content: space-between;
			border-left: 1px solid rgba(255, 255, 255, 0.5);
			border-bottom: 1px solid rgba(255, 255, 255, 0.3);
			border-right: 1px solid rgba(255, 255, 255, 0.3);
			backdrop-filter: blur(10px);
			background: rgba(50, 50, 100, 0.45);

			.item {
				display: flex;
				flex-direction: column;
				align-items: center;
				justify-content: center;

				.img_view {
					width: 60px;
					height: 60px;
					border-radius: 100%;
					display: flex;
					align-items: center;
					justify-content: center;

					.image {
						width: 50px;
						height: 50px;
						overflow: hidden;
					}
				}

				.txt {
					margin-bottom: 15px;
					font-size: 14px;
					font-weight: 900;
					color: rgba(240, 240, 240, 1.0);
				}
			}
		}

		.s_menu {
			display: flex;
			flex-direction: row;
			align-items: stretch;
			justify-content: space-between;
			margin-top: 15px;
			margin-left: 10px;
			margin-right: 10px;
			padding-left: 10px;
			padding-right: 10px;

			.item {
				display: flex;
				flex-direction: column;
				align-items: center;
				justify-content: center;

				.image {
					width: 35px;
					height: 35px;
				}

				.txt {
					margin-top: 5px;
					font-size: 14px;
					color: rgba(51, 51, 51, 1);
				}
			}
		}

		.ad {
			width: 100%;
			display: flex;
			flex-direction: row;
			align-items: center;
			justify-content: center;

			.bg {
				position: absolute;
				width: 100px;
				height: 50px;
				left: 0;
			}

			.ad_btn {
				width: 40%;
				height: 43px;
				margin: 10px;
				background: linear-gradient(0deg, rgba(253, 155, 28, 1), rgba(170, 255, 0, 1.0));
				border-radius: 67px;
				display: flex;
				flex-direction: column;
				align-items: center;
				justify-content: center;



			}
		}
	}
</style>
