import {AfterViewInit, Component, ViewChild} from '@angular/core';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import { StoryService } from '../../service/story/story.service';
import { storyViewModel } from '../../viewModel/storyViewModel';
import { DepartmentService } from '../../service/department/department.service';
@Component({
  selector: 'app-story',
  standalone: true,
  imports: [MatTableModule, MatPaginatorModule],
  templateUrl: './story.component.html',
  styleUrl: './story.component.css'
})
export class StoryComponent implements AfterViewInit {
  constructor (private service: DepartmentService) {}
  displayedColumns: string[] = ['Id','Titulo'];
  dataSource: any;
  departments: any;
  ngOnInit(){
     this.departments = this.service.Get().subscribe(response => {this.departments = response} ,error => {
      console.error('Erro ao obter cidades:', error);});
      this.dataSource = new MatTableDataSource<storyViewModel>(this.departments);
    
   }
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }
}
