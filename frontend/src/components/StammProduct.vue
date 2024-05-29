<<script setup lang="ts">
import { onMounted, ref } from 'vue'
import type { Ref } from 'vue'
import StammProductDto from '@/models/StammProduct'
import StammBrandDto from '@/models/StammBrand'
import {get, post, deleteById } from '@/services/http_client'

const stammdatenListe = ref()
let newEntry: StammProductDto = new StammProductDto({})
const brandList: Ref<StammBrandDto[]> = ref([])

onMounted( async () => {  
  await loadData()
  console.log("sending get request")
  const data: any[] = await get('http://localhost:5067/api/stammdaten/brand')
  console.log("data", data)
  brandList.value = data.map( x => new StammBrandDto(x))
})

async function loadData() {
  const data: any[] = await get('http://localhost:5067/api/stammdaten/product')
  stammdatenListe.value = data.map( x => new StammProductDto(x))
}

async function saveDto() {
  console.log("save", newEntry);
  const data: StammProductDto = await post('http://localhost:5067/api/stammdaten/product', newEntry)
  newEntry = new StammProductDto({})
  const newDb = new StammProductDto(data)
  stammdatenListe.value.unshift(newDb)
}

async function deleteItem(f: StammProductDto) {
  const data: StammProductDto = await deleteById(`http://localhost:5067/api/stammdaten/product/${f.id}`)
  loadData()
}

</script>

<template>
  <h2>Produkte</h2>  

  <div class="row">

  <div>
    <p>Neuer Eintrag</p>

    <div class="row">
      <div class="form-group">          
          <div class="form-group">
              <label>Produkt</label><br>
              <input class="form-control" v-model="newEntry.name">
          </div>
          <div class="form-group">
              <label>Hersteller</label><br>
              <select class="form-select" v-model="newEntry.BrandId">
                <option disabled value="">Bitte wählen</option>
                <option v-for="option in brandList" v-bind:value="option.id" :key="name">{{ option.name }}</option>
              </select>
            </div>

            <div class="form-group">
              <label>Art</label><br>
              <select class="form-select" v-model="newEntry.food_type">
                <option value="Nassfutter">Nassfutter</option>
                <option value="Hartfutter">Hartfutter</option>
              </select>
            </div>

      </div>
    </div>
    <button class="btn btn-primary" @click="saveDto()">Speichern</button>

</div>


</div>

<table class="table table-bordered" style="width:800px;margin-top:30px">
  <thead>
    <tr class="table-dark">
      <th>ID</th>
      <th>Name</th>     
      <th>Marke ID</th>      
      <th>Löschen</th>
    </tr>
  </thead>
  <tbody>
    <tr v-for="prod in stammdatenListe" :key="id">
      <td>{{ prod.id }}</td>
      <td> {{ prod.name}}</td>  
      <td> {{ prod.BrandId }}</td>    
      <td @click="deleteItem(prod)" style="cursor:pointer"><font-awesome-icon :icon="['fa-solid', 'fa-trash']" /></td>
    </tr>
  </tbody>
</table>

</template>