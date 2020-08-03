<Query Kind="Program">
  <Reference>D:\Program Files (x86)\Steam\steamapps\common\Shadowrun Dragonfall Director's Cut\Dragonfall_Data\Plugins\Ionic.Zip.CF.dll</Reference>
  <Reference>D:\Program Files (x86)\Steam\steamapps\common\Shadowrun Dragonfall Director's Cut\Dragonfall_Data\Plugins\NGettext.dll</Reference>
  <Reference>D:\Program Files (x86)\Steam\steamapps\common\Shadowrun Dragonfall Director's Cut\Dragonfall_Data\Plugins\protobuf-net.dll</Reference>
  <Reference>D:\Program Files (x86)\Steam\steamapps\common\Shadowrun Dragonfall Director's Cut\Dragonfall_Data\Plugins\ShadowrunDTO.dll</Reference>
  <Reference>D:\Program Files (x86)\Steam\steamapps\common\Shadowrun Dragonfall Director's Cut\Dragonfall_Data\Plugins\ShadowrunSerializer.dll</Reference>
  <Reference>D:\Program Files (x86)\Steam\steamapps\common\Shadowrun Dragonfall Director's Cut\Dragonfall_Data\Plugins\VTD-Nav.dll</Reference>
  <Namespace>isogame</Namespace>
</Query>

void Main()
{

	//var filestream = File.OpenRead(@"D:\OneDrive\Desktop\New folder\5e1a2d2609034e55ba9c0b56cedfaacb.sav");
	var filestream = File.OpenRead(@"D:\OneDrive\Desktop\New folder\a53115f7bb2048dc88f40cac22d61eee.sav");
	var x = new ShadowrunSerializer();
	var savegame = new SaveGame();
	x.Deserialize(filestream,savegame,typeof(SaveGame));
	
	savegame.story_data.SelectMany(s => s.newsave_party.Where(n => n.char_name == "Jett").Select(nsp => nsp)).Dump(1);
	savegame.story_data.SelectMany(s => s.newsave_party.Where(n => n.char_name == "Jett").Select(nsp => nsp))
		.ToList().ForEach(s =>
			{ 
				s.character_mod.stats.magic_essence = 8;
				s.character_mod.stats.magic = 8;
				s.character_mod.stats.ap_mod = 2;
			
			});
	//savegame.story_data[1].newsave_party[0].character_mod.stats.magic_essence = 10;
	var deststream = File.OpenWrite(@"D:\OneDrive\Desktop\New folder\a53115f7bb2048dc88f40cac22d61eee.savx");
	x.Serialize(deststream,savegame);
	
}

// Define other methods and classes here
