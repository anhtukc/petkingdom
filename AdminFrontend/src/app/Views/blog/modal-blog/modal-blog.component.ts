import { Component, EventEmitter, Input, OnInit, Output, TemplateRef, ViewChild } from '@angular/core';
import { Blog } from 'src/app/Class/BLog';
import { BlogCategory } from 'src/app/Class/BlogCategory';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ApiBlogService } from '../api-blog.service';
import { ApiBlogCategoryService } from '../../blog-category/api-blog-category.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-modal-blog',
  templateUrl: './modal-blog.component.html',
  styleUrls: ['./modal-blog.component.css']
})
export class ModalBlogComponent implements OnInit {
  constructor(private modalService: NgbModal,
    private api: ApiBlogService,
    private apiBlogCategory: ApiBlogCategoryService) { }

  ngOnInit(): void {
    this.apiBlogCategory.getAll().subscribe(result => {
      this.blogCategory = result.list;
    });
  }

  public Editor = ClassicEditor;
  @Input() statusMeaning;
  @Output() updateEvent = new EventEmitter();
  @Output() createEvent = new EventEmitter();

  @ViewChild('modalBlog') optionModal: TemplateRef<any>;
  blogCategory: BlogCategory[] = [];
  public blog: Blog = {
    id: '',
    name: '',
    content: '',
    status: 0,
    thumbnail:'',
    author:'',
    blogCategoryName: '',
    blogCategoryId:''
  };
  public typeModal = '';
  public formGroup = new FormGroup({
    name: new FormControl(this.blog.name, [
      Validators.required,
      Validators.minLength(4)
    ]),
    content: new FormControl(this.blog.content, [
      Validators.required,
      Validators.minLength(10)
    ]),
    blogCategoryId: new FormControl(this.blog.blogCategoryId,[
      Validators.required,
    ]),
    author: new FormControl(this.blog.author,[
      Validators.required,
    ])
  });


  public name = this.formGroup.get('name');
  public author = this.formGroup.get('author');
  public content = this.formGroup.get('content');
  public blogCategoryId = this.formGroup.get('blogCategoryId');
  
  upload(event: Event) {
    const target = event.target as HTMLInputElement;
    const files = target.files as FileList;
    this.blog.file = files[0];
    const reader = new FileReader();
    reader.readAsDataURL(this.blog.file);
    reader.onload = () => {
      this.blog.thumbnail = reader.result as string;
    };
    
  }
  getBlogCategory(){
    for(let i = 0;i <this.blogCategory.length;i++){
      if(this.blogCategory[i].id == this.blog.blogCategoryId){
        this.blog.blogCategoryName = this.blogCategory[i].name;
      }
    }
  }

  public createNew() {
    this.blog = {
      id: '',
      name: '',
      content: '',
      status: 0,
      thumbnail:'',
      author:'',
      blogCategoryName: '',
      blogCategoryId:''
    };
  }

  public getById(id: string) {
    this.api.getById(id).subscribe(
      result => {
        this.blog = result.obj;
      }
    )
  }

  public async create() {
    this.getBlogCategory();
    if (this.formGroup.valid) {
      this.api.addNew(this.blog).subscribe(result => {
        if (result.status == 0) {
          alert("Thêm mới thất bại")
        }
        if (result.status == 1) {
          alert("Thêm mới thành công")
          this.blog = result.obj;
          this.createEvent.emit(this.blog);
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
    this.getBlogCategory();
    if (this.formGroup.valid) {
      this.api.update(this.blog).subscribe(result => {
        if (result.status == 0) {
          alert("Sửa thất bại")
        }
        if (result.status == 1) {
          alert("Sửa thành công")
          this.blog = result.obj;
          this.updateEvent.emit(this.blog);
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
