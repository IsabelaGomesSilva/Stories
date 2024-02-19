import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import { StoryService } from '../../service/story/story.service';
import { storyViewModel } from '../../viewModel/storyViewModel';
import { DepartmentService } from '../../service/department/department.service';
import { response } from 'express';
import { departmentViewModel } from '../../viewModel/departmentViewModel';

@Component({
  selector: 'app-story',
  standalone: true,
  imports: [MatTableModule, MatPaginatorModule],
  templateUrl: './story.component.html',
  styleUrl: './story.component.css'
})
export class StoryComponent implements OnInit, AfterViewInit {
  constructor (private service: StoryService) {}
  
  displayedColumns: string[] = ['Id', 'Title', 'Description'];
 
  dataSource: MatTableDataSource<storyViewModel> = new MatTableDataSource<storyViewModel>();
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngOnInit(){
      this.service.Get().subscribe((response: storyViewModel[])  => {
        console.log(response);
        if(response)
        {
          this.dataSource.data = response;
          this.ngAfterViewInit();
        }
      } ,error => {
      console.error('Erro ao obter cidades:', error);});
      console.log(this.dataSource);
   }
   ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
  }
}
