import { Routes } from '@angular/router';
import { StoryComponent } from './view/story/story.component';
import { HomeComponent } from './view/home/home.component';
export const routes: Routes = [
  {path:'', component: HomeComponent},
  {path:'Stories', component: StoryComponent}
];
