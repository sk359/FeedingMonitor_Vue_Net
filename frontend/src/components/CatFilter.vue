<script setup lang="ts">
import { onMounted, ref, watch } from 'vue'
import type { Ref } from 'vue'

const props = defineProps<{
  nameList: string[]
}>()
const emit = defineEmits(({
    filter(names:string[]) {return true; }
}))

let checkedNames: Ref<string[]> = ref([])

watch(props, () => { // onChange fuer props
    checkedNames.value = props.nameList
})

function emitNames() {    
    emit('filter', checkedNames.value)
}


</script>

<template>
    <div style="display:inline-flex;align-items: center;">
      <div v-for="name in nameList">
        <input type="checkbox" :id="name" :value="name" v-model="checkedNames">
        <label :for="name">{{name}}</label>
      </div>
      <button style="margin-left: 10px;" class="btn btn-primary" @click="emitNames()">Filtern</button>
    </div>
</template>

<style scoped>
</style>