<script setup lang="ts">
import { onMounted, ref, computed } from 'vue'
import {get, post, deleteById } from '@/services/http_client'
import type { Ref } from 'vue'
import { FeedingDto } from '@/models/Feeding'
import StammBrandDto from '@/models/StammBrand'
import CatFilter from '@/components/CatFilter.vue'
defineProps<{
  msg: string
}>()

let feedingList: Ref<FeedingDto[]> = ref([])
const brandList = ref()
let newEntry: Ref<FeedingDto> = ref(new FeedingDto({}))
let catNames = ref([])
let selectedFilters: Ref<string[]> = ref([])

const visibleRows = computed(() => {
  if (selectedFilters) {
    return feedingList.value.filter((n) => selectedFilters.value.includes(n.catname))
  }
  return []
})

onMounted( async () => {
  console.log("sending get request")
  const data: any[] = await get('http://localhost:5067/api/stammdaten/brand')
  brandList.value = data.map( x => new StammBrandDto(x))
  loadData()
})

async function loadData() {
  catNames = ref([])
  const data: any[] = await get('http://localhost:5067/api/bewegdaten')
  feedingList.value = data.map( x => new FeedingDto(x))
  updateCatNames();
}

function updateCatNames() {
  for (let item of feedingList.value) {
    if (!catNames.value.includes(item.catname)) {
      catNames.value.push(item.catname)
    }
  }
  selectedFilters = ref(catNames.value)

}

async function saveDto() {
  console.log("save", newEntry);
  const data: FeedingDto = await post('http://localhost:5067/api/bewegdaten', newEntry.value)
  newEntry = ref(new FeedingDto({}))
  const newDb = new FeedingDto(data)
  feedingList.value.unshift(newDb)
  updateCatNames();
}

async function deleteItem(f: FeedingDto) {
  const data: FeedingDto = await deleteById(`http://localhost:5067/api/bewegdaten/${f.id}`)
  loadData()
}

function isFish(feeding: FeedingDto): boolean {
  return feeding.taste.toLowerCase().includes('fisch'); 
}

function disableSave(): boolean {  
  if (newEntry == null) {
    return true;
  }  
  if (newEntry.value.catname && newEntry.value.brandname && newEntry.value.eatenpercentage &&
    newEntry.value.feedingtime) {
      return false;
    }
  
  return true;
}

function filterByNames(names: string[]) {
  selectedFilters.value = names      
}

function showTableRow(feeding: FeedingDto): boolean {  
  return selectedFilters.value.includes(feeding.catname)
}


</script>

<template>

  <div class="row">

    <div>
    <div class="row">
        <div class="col-6">
            <div class="form-group">
              <label>Name der Katze</label><br>
              <input class="form-control" v-model="newEntry.catname">
            </div>            
            <div class="form-group">
              <label>Hersteller</label><br>
              <select class="form-select" v-model="newEntry.brandname">
                <option disabled value="">Bitte wählen</option>
                <option v-for="option in brandList" v-bind:value="option.name" :key="name">{{ option.name }}</option>
              </select>
            </div>
            <div class="form-group">
                <label>Produkt</label><br>
                <input class="form-control" v-model="newEntry.productname">
            </div>
        </div>

        <div class="col-6">
            <div class="form-group">
                <label>Geschmack</label><br>
                <input class="form-control" v-model="newEntry.taste">
            </div>
            <div class="form-group">
                <label>Zu wieviel % aufgegessen</label><br>
                <input class="form-control" type="number" min="0" max="100" v-model="newEntry.eatenpercentage">
            </div>
            <div class="form-group">
                <label>Art</label><br>
                <input class="form-control" min="0" max="100" v-model="newEntry.type">
            </div>
            <div class="form-group">
                <label>Wann</label><br>
                <input class="form-control" type="datetime-local"  v-model="newEntry.feedingtime">
            </div>
        </div>
    </div>
    <button :disabled="disableSave()"  class="btn btn-primary" @click="saveDto()">Speichern</button>

</div>

<div style="margin-top:20px">
  <CatFilter :name-list="catNames" @filter="(names) => filterByNames(names)"/>
</div>


  </div>

  <table class="table table-bordered" style="width:800px;margin-top:30px">
    <thead>
      <tr class="table-dark">
        <th scope="col">Katze</th>
        <th>Marke</th>
        <th>Produkt</th>
        <th>Geschmack</th>
        <th>Gegessen %</th>
        <th>Zeit</th>
        <th>Löschen</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="feeding in visibleRows" :key="id" :class="{fish: isFish(feeding)}"
      :title="isFish(feeding) ? 'Fisch' : ''">
        <td>{{ feeding.catname }}</td>
        <td> {{ feeding.brandname }}</td>
        <td> {{ feeding.productname }}</td>
        <td> {{ feeding.taste }}</td>
        <td> {{ feeding.eatenpercentage }}</td>
        <td> {{ feeding.feedingTimeGerman() }}</td>
        <td @click="deleteItem(feeding)" style="cursor:pointer"><font-awesome-icon :icon="['fa-solid', 'fa-trash']" /></td>
      </tr>
    </tbody>
  </table>
</template>

<style scoped>
.fish td {
  background-color: lightblue;
}
.item {
  margin-top: 2rem;
  display: flex;
  position: relative;
}

.details {
  flex: 1;
  margin-left: 1rem;
}

i {
  display: flex;
  place-items: center;
  place-content: center;
  width: 32px;
  height: 32px;

  color: var(--color-text);
}

h3 {
  font-size: 1.2rem;
  font-weight: 500;
  margin-bottom: 0.4rem;
  color: var(--color-heading);
}

@media (min-width: 1024px) {
  .item {
    margin-top: 0;
    padding: 0.4rem 0 1rem calc(var(--section-gap) / 2);
  }

  i {
    top: calc(50% - 25px);
    left: -26px;
    position: absolute;
    border: 1px solid var(--color-border);
    background: var(--color-background);
    border-radius: 8px;
    width: 50px;
    height: 50px;
  }

  .item:before {
    content: ' ';
    border-left: 1px solid var(--color-border);
    position: absolute;
    left: 0;
    bottom: calc(50% + 25px);
    height: calc(50% - 25px);
  }

  .item:after {
    content: ' ';
    border-left: 1px solid var(--color-border);
    position: absolute;
    left: 0;
    top: calc(50% + 25px);
    height: calc(50% - 25px);
  }

  .item:first-of-type:before {
    display: none;
  }

  .item:last-of-type:after {
    display: none;
  }
}
</style>
