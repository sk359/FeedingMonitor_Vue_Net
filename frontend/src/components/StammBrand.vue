<script setup lang="ts">
import { onMounted, ref } from 'vue'
import StammBrandDto from '@/models/StammBrand'
import {get, post, deleteById } from '@/services/http_client'

const stammdatenListe = ref()
let newEntry: StammBrandDto = new StammBrandDto({})

onMounted( async () => {
  console.log("sending get request")
  loadData()
})

async function loadData() {
  const data: any[] = await get('http://localhost:5067/api/stammdaten/brand')
  stammdatenListe.value = data.map( x => new StammBrandDto(x))
}

async function saveDto() {
  console.log("save", newEntry);
  const data: StammBrandDto = await post('http://localhost:5067/api/stammdaten/brand', newEntry)
  newEntry = new StammBrandDto({})
  const newDb = new StammBrandDto(data)
  stammdatenListe.value.unshift(newDb)
}

async function deleteItem(f: StammBrandDto) {
  const data: StammBrandDto = await deleteById(`http://localhost:5067/api/stammdaten/brand/${f.id}`)
  loadData()
}

</script>

<template>
  <h2>Marken</h2>  

  <div class="row">

  <div>
    <p>Neuer Eintrag</p>

    <div class="row">
      <div class="form-group">          
          <div class="form-group">
              <label>Hersteller</label><br>
              <input class="form-control" v-model="newEntry.name">
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
      <th>Produkte</th>      
      <th>Löschen</th>
    </tr>
  </thead>
  <tbody>
    <tr v-for="marke in stammdatenListe" :key="id">
      <td>{{ marke.id }}</td>
      <td> {{ marke.name}}</td>  
      <td> {{ marke.getProductString() }}</td>    
      <td @click="deleteItem(marke)" style="cursor:pointer"><font-awesome-icon :icon="['fa-solid', 'fa-trash']" /></td>
    </tr>
  </tbody>
</table>

</template>