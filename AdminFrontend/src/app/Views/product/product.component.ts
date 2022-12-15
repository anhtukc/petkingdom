import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { Data } from '@angular/router';
import { Observable } from 'rxjs/internal/Observable';
import { ModalConfig } from 'src/app/Class/ModalConfig';
import { Product } from 'src/app/Class/product';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
 
  @ViewChild('editModal') editModal : TemplateRef<any>; 

  constructor(private modalService: NgbModal) { }

  public openModal(){
    this.modalService.open(this.editModal);
  }

  public data: Product[] = [] ;
  public product_id ="";
 
  public noData: any;
  public page = 0;
  public pageSize = 10;
  ngOnInit(): void {
   
  }
 
}
