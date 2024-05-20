export default class StammBrandDto {

  id: number;
  name: string;
  created: string;
  products: string[];

  constructor(data: any){
    data = data || {};
    this.id = data.id;
    this.name = data.name;
    this.created = data.created;
    this.products = [];
    if (data.products) {
      this.products = data.products.map( (p:any) => p.name);
    }
  }

  getProductString(): string {
    return this.products.join(",");
  }

}
