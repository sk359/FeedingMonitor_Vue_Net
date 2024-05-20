export default class StammProductDto {

  id: number;
  name: string;
  created: string;
  stamm_brand_id: number;

  constructor(data: any){
    data = data || {};
    this.id = data.id;
    this.name = data.name;
    this.created = data.created;
    this.stamm_brand_id = data.stamm_brand_id;
  }

}