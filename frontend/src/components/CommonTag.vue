<template>
  <div class="tags">
    <el-tag v-for="(tag, index) in tags" :key="tag.name"  @click="changeMenu(tag)" @close="handleMenu(tag, index)"
      size="small" :closable="tag.name !== 'home'" :effect="$route.name === tag.name ? 'dark' : 'plain'">
      {{ tag.title }}
    </el-tag>
  </div>
</template>

<script>
import { mapState, mapMutations } from "vuex";
export default {
  nmae: "CommonTap",
  data() {
    return {};
  },
  computed: {
    ...mapState({
      tags: (state) => state.tag.tagList,
    }),
  },
  methods: {
    ...mapMutations({
      close: 'closeTag'
    }),
    changeMenu(tag) {
      this.$router.push({ name: tag.name })
    },
    handleMenu(tag, index) {
      const length = this.tags.length - 1
      // if(tag.name !== this.$route.name){
      //   return;
      // }
      this.close(tag)
      // if (index === length) {
      //   this.$route.psuh({
      //     name: this.tags[index - 1].name
      //   })
      // } else {
      //   this.$route.push({
      //     name: this.tags[index].name
      //   })
      // }
    },
  },
};
</script>

<style lang="less" scoped>
.tags {
  padding: 10px;

  .el-tag {
    margin-right: 15px;
    cursor: pointer
  }
}
</style>