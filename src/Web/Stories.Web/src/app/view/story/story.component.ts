import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import { StoryService } from '../../service/story/story.service';
import { storyViewModel } from '../../viewModel/storyViewModel';
import {MatButtonModule} from '@angular/material/button';


@Component({
  selector: 'app-story',
  standalone: true,
  imports: [MatTableModule, MatPaginatorModule, MatButtonModule],
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
        if(response)
        {
          this.dataSource.data = response;
          this.ngAfterViewInit();
        }
      } ,error => {
      console.error('Erro ao obter cidades:', error);});
   }
   ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
  }
}
