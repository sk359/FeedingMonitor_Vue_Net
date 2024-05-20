<script setup lang="ts">
import { ref } from 'vue'
import StammBrand from '@/components/StammBrand.vue'
import StammProduct from '@/components/StammProduct.vue'

let selectedTable = ref("stamm_brand") // Hier funktioniert ohne ref nicht!


function selectMarke() {  
  selectedTable.value = "stamm_brand"
}

function selectProduct() {  
  selectedTable.value = "stamm_product"
}


</script>


<template>
  <div>
    <div id="stammdaten-top-bar">Stammdaten</div>
    <div id="stammdaten-lower-bar">
      <button class="btn btn-primary" @click="selectMarke()">Marke</button>
      <button style="margin-left: 20px" class="btn btn-primary" @click="selectProduct()">Produkte</button>
    </div>
  </div>

  <div id="stammdaten-row">
    <div v-if="selectedTable == 'stamm_brand'">
      <StammBrand/>
    </div>
    <div v-else-if="selectedTable == 'stamm_product'">
      <StammProduct/>
    </div>
  </div>
  
</template>

<style>
#stammdaten-top-bar {
  width: 100vh;
  padding: 10px;
  background-color: lightgray;
  font-size: 20px;
}

#stammdaten-lower-bar {
  width: 100vh;
  padding: 10px;
  background-color: rgb(230, 203, 203);
}

@media (min-width: 1024px) {
  .about {
    min-height: 100vh;
    display: flex;
    align-items: center;
  }
}
</style>
