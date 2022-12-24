import { Component, Input, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PetServiceImage } from 'src/app/Class/PetServiceImage';
import { ApiServiceImageService } from './api-service-image.service';

@Component({
  selector: 'app-modal-service-image-management',
  templateUrl: './modal-service-image-management.component.html',
  styleUrls: ['./modal-service-image-management.component.css']
})
export class ModalServiceImageManagementComponent implements OnInit {
  @ViewChild('ModalPetServiceImage') optionModal: TemplateRef<any>;
  serviceId = '';
  data: PetServiceImage[] = [];
  previewImagesList: File[] = [];
  imageList: string[] = []
  setServiceId(id: string) {
    this.serviceId = id;
  }
  constructor(private modalService: NgbModal, private api: ApiServiceImageService) { }
  upload(event: Event) {
    const target = event.target as HTMLInputElement;
    const files = target.files as FileList;

    if (files.length > 0) {
      for (let i = 0; i < files.length; i++) {
        this.imageList[i] = URL.createObjectURL(files[i]);
        this.previewImagesList[i] = files[i];
      }
      console.log(this.imageList);
    }
  }

  removePreviewImage(index: number) {
    this.imageList.splice(index, 1);
    this.previewImagesList.splice(index, 1);
  }

  ngOnInit(): void {
  }

  getAllImage() {
    this.api.getAll(this.serviceId).subscribe(result => {
      if (result.status == 1) {
        this.data = result.list;
      }
      else {
        console.log(result.details);
      }
    })
  }

  public openModal(id: string) {
    this.modalService.open(this.optionModal);
    this.serviceId = id;
    this.getAllImage();
  }

  removeImage(id: string) {
    const conf = confirm("Bạn có chắc chắn muốn xóa không?");
    if (conf) {
      this.api.delete(id).subscribe(result => {

        if (result.status == 1) {
          alert("Xóa thành công");
          this.getAllImage();
        }
      })
    }
  }
  addNew() {
    this.api.addNew(this.previewImagesList, this.serviceId).subscribe(result => {
      if (result.status == 1) {
        this.getAllImage();
        alert("Thêm ảnh thành công");
        this.setNewModal();
      }
    })
  }

  setNewModal() {
    this.previewImagesList = [];
    this.imageList = []
  }
}
