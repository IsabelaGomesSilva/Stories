import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import { StoryService } from '../../service/story/story.service';
import { storyViewModel } from '../../viewModel/storyViewModel';
import {MatButtonModule} from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';
import { AddStoryComponent } from './dialogStory/add-story/add-story.component';

@Component({
  selector: 'app-story',
  standalone: true,
  imports: [MatTableModule, MatPaginatorModule, MatButtonModule],
  templateUrl: './story.component.html',
  styleUrl: './story.component.css'
})
export class StoryComponent implements OnInit, AfterViewInit {
  constructor (private storyService: StoryService, private dialog: MatDialog) {}
  
  displayedColumns: string[] = ['Id', 'Title', 'Description', 'Department'];
 
  dataSource: MatTableDataSource<storyViewModel> = new MatTableDataSource<storyViewModel>();
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngOnInit(){
    this.storyService.Get().subscribe((response: storyViewModel[])  => {
        if(response)
        {
          this.dataSource.data = response;
          this.ngAfterViewInit();
        }
      } ,error => { console.error('Erro ao obter histórias:', error);});
   }

  ngAfterViewInit(): void { this.dataSource.paginator = this.paginator; }
  
  openDialog(enterAnimationDuration: string, exitAnimationDuration: string): void {
    this.dialog.open(AddStoryComponent, {
    
      enterAnimationDuration,
      exitAnimationDuration,
    });
  }
 
}
