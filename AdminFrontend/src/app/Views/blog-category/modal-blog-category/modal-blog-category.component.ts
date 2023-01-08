import { Component, EventEmitter, Input, OnInit, Output, TemplateRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { BlogCategory } from 'src/app/Class/BlogCategory';
import { ApiBlogCategoryService } from '../api-blog-category.service';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
@Component({
  selector: 'app-modal-blog-category',
  templateUrl: './modal-blog-category.component.html',
  styleUrls: ['./modal-blog-category.component.css']
})
export class ModalBlogCategoryComponent implements OnInit {
  constructor(private modalService: NgbModal,
    private api: ApiBlogCategoryService,
) { }

  ngOnInit(): void {
   
  }

  public Editor = ClassicEditor;
  @Input() statusMeaning;
  @Output() updateEvent = new EventEmitter();
  @Output() createEvent = new EventEmitter();

  @ViewChild('modalBlogCategory') optionModal: TemplateRef<any>;

  public blogCategory: BlogCategory = {
    id: '',
    name: '',
    description: '',
    status: 0,
    createdDate: null,
    updateDate: null,
  };
  public typeModal = '';
  public formGroup = new FormGroup({
    name: new FormControl(this.blogCategory.name, [
      Validators.required,
      Validators.minLength(4)
    ]),
    description: new FormControl(this.blogCategory.description, [
      Validators.required,
      Validators.minLength(10)
    ]),
    

  });


  public name = this.formGroup.get('name');
  public description = this.formGroup.get('description');


  public createNew() {
    this.blogCategory = {
      id: '',
      name: '',
      description: '',
      status: 0,
      createdDate: null,
      updateDate: null,
    };
  }

  public getById(id: string) {
    this.api.getById(id).subscribe(
      result => {
        this.blogCategory = result.obj;
      }
    )
  }

  public async create() {

    if (this.formGroup.valid) {
      this.api.addNew(this.blogCategory).subscribe(result => {
        if (result.status == 0) {
          alert("Thêm mới thất bại")
        }
        if (result.status == 1) {
          alert("Thêm mới thành công")
          this.blogCategory = result.obj;
          this.createEvent.emit(this.blogCategory);
        }
      })
    }
    else {
      Object.keys(this.formGroup.controls).forEach(key => {
        this.formGroup.controls[key].markAsTouched();
        this.formGroup.controls[key].markAsDirty();
      });
    }
  }

  public async update(){

    if (this.formGroup.valid) {
      this.api.update(this.blogCategory).subscribe(result => {
        if (result.status == 0) {
          alert("Sửa thất bại")
        }
        if (result.status == 1) {
          alert("Sửa thành công")
          this.blogCategory = result.obj;
          this.updateEvent.emit(this.blogCategory);
        }
      })
    }
    else {
      Object.keys(this.formGroup.controls).forEach(key => {
        this.formGroup.controls[key].markAsTouched();
        this.formGroup.controls[key].markAsDirty();
      });
    }
  }

  public openModal(typeModal: string, id?: string) {
    this.typeModal = typeModal;
    this.modalService.open(this.optionModal);
    if (typeModal == 'create') {
      this.createNew();
    }
    if (typeModal == 'edit') {
      this.getById(id);
    }
  }

}
