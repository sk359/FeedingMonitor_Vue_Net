export class FeedingDto {

  id: number;
  catname: string;
  brandname: string;
  productname: string;
  taste: string;
  type: string;
  eatenpercentage: number;
  created: string;
  feedingtime: string;

  constructor(data: any){
    data = data || {};
    this.id = data.id;
    this.catname = data.catname;
    this.brandname = data.brandname;
    this.productname = data.productname;
    this.taste = data.taste;
    this.type = data.type;
    this.eatenpercentage = data.eatenpercentage;
    this.created = data.created;
    this.feedingtime = data.feedingtime;
  }

  feedingTimeGerman(): string {
    const date = new Date(this.feedingtime);
    return date.toLocaleString();
  }

}
