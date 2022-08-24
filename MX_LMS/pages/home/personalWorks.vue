<template>
	<view>
		<view class="list">
			<uni-list>
				<view class="classCounselor" v-for="(item,index) in workInfo" :key=index
					@click="changePage(item.id,item.title)">
					<uni-list-item :title="item.title" :note="item.note" :thumb="item.thumb" thumbSize="lg" showArrow>
					</uni-list-item>
				</view>
			</uni-list>
		</view>
	</view>
</template>

<script>
	export default {
		data() {
			return {
				workInfo: [{
					id: '1',
					title: '作品一',
					note: '这是一个作品',
					thumb: '@/static/logo.png'
				}, {
					id: '2',
					title: '作品一',
					note: '这是一个作品',
					thumb: '@/static/logo.png'
				}, ]
			}
		},
		methods: {
			changePage(id, title) {
				console.log(id);
				console.log(title);
				// 传入作品详情页的数据，只能传入数组
				let data = JSON.stringify({
					id: id,
					title: title
				})
				uni.navigateTo({
					url: "/pages/work/workDetails?data=" + data
				});
			}
		}
	}
</script>

<style>

</style>
