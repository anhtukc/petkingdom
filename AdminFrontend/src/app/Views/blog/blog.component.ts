import { Component, OnInit, ViewChild } from '@angular/core';
import { Blog } from 'src/app/Class/BLog';
import { BlogCategory } from 'src/app/Class/BlogCategory';
import { pagination } from 'src/app/Class/pagination';
import { basedSearchObject } from 'src/app/Class/SearchObjects';
import { sortingService } from 'src/app/Helper/sorting-helper';
import { ApiBlogCategoryService } from '../blog-category/api-blog-category.service';
import { ApiBlogService } from './api-blog.service';
import { ModalBlogComponent } from './modal-blog/modal-blog.component';

@Component({
  selector: 'app-blog',
  templateUrl: './blog.component.html',
  styleUrls: ['./blog.component.css']
})
export class BlogComponent implements OnInit {

  constructor(private api: ApiBlogService,private apiBlogCategory:ApiBlogCategoryService,
    private sort: sortingService) { }

  ngOnInit(): void {
    this.apiBlogCategory.getAll().subscribe(result=>{
      this.blogCategory = result.list;
      
    })
    this.fetchData();
 
  }
  public serviceOptionId ='';
  statusMeaning: Array<string> = ["Không hoạt động", "Đang hoạt động"];
  public page: pagination = {
    currentPage: 1,
    pageSize: 20,
    sortColumn: "name",
    sortOrder: "ASC"
  };
  public totalData = 0;
  public data: Blog[] = [];
  public blogCategory: BlogCategory[] = [];
  public searchObject: basedSearchObject = {
    name: '',
    status: -1
  };

  @ViewChild(ModalBlogComponent) modalBLog!:ModalBlogComponent;
  isSearchDivVisible: boolean = false;
  isSearching: boolean = false;

 
  fetchData() {
    if (this.isSearching == true) {
      this.api.searchPage(this.page, this.searchObject).subscribe(
        result => {

          if (result.status == 1) {

            this.data = result.list;
            this.totalData = result.numberOfRecords;
          }
        })
    }
    else {
      this.api.getPage(this.page).subscribe((result) => {
        this.data = result.list;
        this.totalData = result.numberOfRecords;
      }
      );
    }
  }

  renderPage(event: number) {
    this.page.currentPage = event;
    this.fetchData();
  }

  GetSortColumn(column: string) {
    if (this.page.sortColumn != column) {
      this.page.sortOrder = '';
    }
    this.page.sortColumn = column;
    this.page.sortOrder = this.sort.changeSortOrder(this.page.sortOrder);
    this.fetchData();
  }

  addNew(blog: Blog) {
    this.data.push(blog);
  }
  update(blog: Blog) {
    for (let i = 0; i < this.data.length; i++) {
      if (this.data[i].id == blog.id) {
        this.data[i] = blog;
        break;
      }
    }
  }
  
  delete(id: string) {
    const cf = confirm("Bạn có chắc chắn muốn xóa không?");
    if (cf) {
      this.api.delete(id).subscribe(result => {
        if (result.status == 0) {
          alert("Không tìm được đối tượng");
          console.log(result.details);
        }
        if (result.status == 1) {
          alert("Xóa thành công");
          this.deleteDataInBrowser(id);
        }
      })
    }
  }

  deleteDataInBrowser(id: string) {
    for (let i = 0; i < this.data.length; i++) {
      if (this.data[i].id === id) {
        this.data.splice(i, 1);
        this.totalData -= 1;
      }
    }
  }


  changeExtendSearchDivVisible() {
    this.isSearchDivVisible = !this.isSearchDivVisible;
  }

  search() {
    this.isSearching = true;
    this.page.currentPage = 1;
    this.fetchData();
  }

  resetSearching() {
    this.isSearching = false;
    this.page.currentPage = 1;
    this.fetchData();
  }

  openModal(typeModal:string,id?:string){
    if(typeModal=='create'){
      this.modalBLog.openModal(typeModal);
    }
    if(typeModal=='edit'){
      this.modalBLog.openModal(typeModal,id);
    }
  }
  

}
