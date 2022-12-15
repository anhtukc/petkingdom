import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-service-options',
  templateUrl: './service-options.component.html',
  styleUrls: ['./service-options.component.css']
})
export class ServiceOptionsComponent implements OnInit {
  @ViewChild('optionModal') optionModal : TemplateRef<any>; 

  constructor(private modalService: NgbModal) { }

  public openModal(){
    this.modalService.open(this.optionModal);
  }

  ngOnInit(): void {
  }

}
