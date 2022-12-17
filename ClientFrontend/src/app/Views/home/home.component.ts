import { Component, OnInit, ViewChild } from '@angular/core';
import { NgImageSliderComponent } from 'ng-image-slider';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

images =['https://storage.cloud.google.com/pet-kingdom-resource/multi-pet-discount.jpg',
'https://storage.cloud.google.com/pet-kingdom-resource/slider-bg.webp'
]
  constructor() { }

  ngOnInit(): void {
  }

}
