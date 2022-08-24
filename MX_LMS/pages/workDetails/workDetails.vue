<template>
	<view>
		<view class="title1">
			<uni-title type="h1" title="作品标题" align="center"></uni-title>
		</view>
		
		<view class="title2">
			<uni-title type="h8" title="作者" align="left"></uni-title>
		</view>
		
		<view class="content">
			<uni-title v-for="item in substance" 
			:title="item.name" 
			note="Tips" 
			align="center"
			:key="item.name" class="a">
				<!-- <text></text> -->
				{{item.score}}
			</uni-title>
		</view>
		<!-- <uni-swiper-dot :current="current" field="content">
			<swiper class="swiper-box" @change="change">
				<swiper-item v-for="(item ,index) in info" :key="index">
					<view class="swiper-item">
						 
					</view>
				</swiper-item>
			</swiper>
		</uni-swiper-dot> -->
		<view class="picture">
			<image class='img' src='../../static/md_door.jpg' mode="widthFix" align="center"></image>
		</view>
	</view>
</template>

<script>
	export default {
		data() {
			return {
				substance:
				[
					{
						name:'一、对于 Web 性能优化，您有哪些了解和经验吗？一、对于 Web 性能优化，您有哪些了解和经验吗？'
					}
				],
				info: [{
								// content: '内容 A'
						}, ],
							current: 0,
			}
		},
		methods: {
			change(e) {
						this.current = e.detail.current;
					}
		}
	}
</script>

<style>
	.title1{
		margin-bottom:0px;
	}
	.title2{
		display: block;
		margin-left: 5px;
		margin-bottom: 5px;
	}
	image{
		display: block;
		margin: 0 auto;
		width: 80%;
	}
	.a{
		font-size: 28rpx;
		     font-weight: 400;
		     color: rgba(51, 51, 51, 1); 
		     line-height: 50rpx;
		     text-indent: 60rpx;
		     text-align:justify;
		     text-align-last:left;
	}
	.content{
		 padding: 10upx 34upx;
	}
</style>
