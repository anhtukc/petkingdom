import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiPetServiceImage } from 'src/app/api/pet-service-image/pet-service-image.service';
import { ApiPetService } from 'src/app/api/pet-service/pet-service.service';
import { ApiServiceOption } from 'src/app/api/service-option/service-option.service';
import { ApiServicePrice } from 'src/app/api/service-price/service-price.service';
import { petService } from 'src/app/Class/pet-service';
import { PetServiceImage } from 'src/app/Class/PetServiceImage';
import { ServiceOption } from 'src/app/Class/PetServiceOptions';
import { ServiceSellPrice } from 'src/app/Class/ServiceSellPrice';


@Component({
  selector: 'app-service-single',
  templateUrl: './service-single.component.html',
  styleUrls: ['./service-single.component.css']
})
export class ServiceSingleComponent implements OnInit {
  options: ServiceOption[] = [];
  selectedOptionId: string = '';
  serviceSellPrices: ServiceSellPrice[] = [];
  image:PetServiceImage[] = [];
  petservice:petService ={

  }
  constructor(private apiPetService:ApiPetService,
    private apiOption:ApiServiceOption,
    private apiServicePrice:ApiServicePrice,
    private apiPetServiceImage:ApiPetServiceImage,
    private route: ActivatedRoute) { }


  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.apiPetServiceImage.getById(id!).subscribe(result=>{
      this.image = result.list;
    })
    this.apiOption.getOption(id!).subscribe(options => {
      this.options = options.list
      console.log(this.options);
    });
    this.apiPetService.getById(id!).subscribe(result=>{
      this.petservice = result.obj
    })
    
  }

  onOptionIdChange(): void {
      if(this.selectedOptionId != ''){
        this.apiServicePrice.getSellPrice(this.selectedOptionId).subscribe(result=>{
          this.serviceSellPrices = result.list
      })
      }
  }
}
