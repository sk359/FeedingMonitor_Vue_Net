export default class StammProductDto {

  id: number;
  name: string;
  food_type: string;
  created: string;
  BrandId: number;

  constructor(data: any){
    data = data || {};
    this.id = data.id;
    this.name = data.name;
    this.food_type = data.food_type;
    this.created = data.created;
    this.BrandId = data.BrandId;
  }

}