import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import { StoryService } from '../../service/story/story.service';
import { storyViewModel } from '../../viewModel/storyViewModel';
import {MatButtonModule} from '@angular/material/button';
import { DepartmentService } from '../../service/department/department.service';
import { departmentViewModel } from '../../viewModel/departmentViewModel';


@Component({
  selector: 'app-story',
  standalone: true,
  imports: [MatTableModule, MatPaginatorModule, MatButtonModule],
  templateUrl: './story.component.html',
  styleUrl: './story.component.css'
})
export class StoryComponent implements OnInit, AfterViewInit {
  constructor (private storyService: StoryService, private departmentsService: DepartmentService ) {}
  
  displayedColumns: string[] = ['Id', 'Title', 'Description', 'Department'];
 
  dataSource: MatTableDataSource<storyViewModel> = new MatTableDataSource<storyViewModel>();
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  departments!: departmentViewModel[];

  ngOnInit(){
    this.getDepartments();
    
    this.storyService.Get().subscribe((response: storyViewModel[])  => {
        if(response)
        {
          this.dataSource.data = response;
          this.ngAfterViewInit();
        }
      } ,error => {
      console.error('Erro ao obter histórias:', error);});

      
   }
   ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
  }
  getDepartments() {
    this.departmentsService.Get().subscribe((response: departmentViewModel[] )  => {this.departments = response},error => {
      console.error('Erro ao obter departaments:', error);} );
      console.log(this.departments)
     
  }
}
