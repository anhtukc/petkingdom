import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BlogCategoryComponent } from './blog-category.component';
import { HttpClientModule } from '@angular/common/http';
import { NgxPaginationModule } from 'ngx-pagination';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { ModalBlogCategoryComponent } from './modal-blog-category/modal-blog-category.component';



@NgModule({
  declarations: [
    BlogCategoryComponent,
    ModalBlogCategoryComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    NgxPaginationModule,
    FormsModule,
    BrowserAnimationsModule,
    NgbModule,
    ReactiveFormsModule,
    CKEditorModule
  ]
})
export class BlogCategoryModule { }
