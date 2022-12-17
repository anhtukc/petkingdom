import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-product-single',
  templateUrl: './product-single.component.html',
  styleUrls: ['./product-single.component.css']
})
export class ProductSingleComponent implements OnInit {
  images =['https://storage.cloud.google.com/pet-kingdom-resource/multi-pet-discount.jpg',
  'https://storage.cloud.google.com/pet-kingdom-resource/slider-bg.webp'
  ]
  constructor() { }

  ngOnInit(): void {
  }

}
